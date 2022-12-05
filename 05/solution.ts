// Input

const text = await Deno.readTextFile("./input.txt");
const [initialSetup, rearrangements] = text
  .split("\n\n")
  .map((e) => e.split("\n"));

// Part 1

function initializeData(initial: string[]) {
  const crateInputs = initial.slice(0, initial.length - 1);

  const stacks: string[][] = Array.from(
    { length: Math.ceil(crateInputs[0].length / 4) },
    (_, _i) => new Array(0)
  );

  const re = /[A-Z]/;

  for (let i = 0; i < crateInputs.length; i++) {
    for (let j = 0; j < crateInputs[i].length; j += 4) {
      const crate = crateInputs[i].substring(j, j + 3);
      const regex = re.exec(crate);

      if (regex) stacks[j / 4].push(regex[0]);
    }
  }

  stacks.forEach((e) => e.reverse());

  return stacks;
}

const firstStacks = initializeData(initialSetup);

const numberRe = /\d+/g;

for (const rearrangement of rearrangements) {
  const [count, from, to] = rearrangement
    .match(numberRe)!
    .map((e) => parseInt(e));

  for (let i = 0; i < count; i++) {
    const crate = firstStacks[from - 1].pop()!;
    firstStacks[to - 1].push(crate);
  }
}

let firstMessage = "";

for (const stack of firstStacks) firstMessage += stack[stack.length - 1];

console.log(firstMessage);

// Part 2

const secondStacks = initializeData(initialSetup);

for (const rearrangement of rearrangements) {
  const [count, from, to] = rearrangement
    .match(numberRe)!
    .map((e) => parseInt(e));

  const fromArray = secondStacks[from - 1];

  const crates = fromArray.slice(fromArray.length - count);

  secondStacks[from - 1] = [...fromArray.slice(0, fromArray.length - count)];

  secondStacks[to - 1] = [...secondStacks[to - 1], ...crates];
}

let secondMessage = "";

for (const stack of secondStacks) secondMessage += stack[stack.length - 1];

console.log(secondMessage);
