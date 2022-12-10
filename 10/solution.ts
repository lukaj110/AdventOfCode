// Input

const text = await Deno.readTextFile("./input.txt");

const input = text.split("\n");

// Part 1

const breakPoints = [20, 60, 100, 140, 180, 220];

let currentInstruction = 0;

let cycle = 0;

let register = 1;

let signalStrengthSum = 0;

const drawing: string[][] = Array.from({ length: 6 }, (_, _i) => []);

for (let i = 0; i < input.length; i++) {
  const [instruction, value] = input[i].split(" ");

  /* Part 2 start */

  const row = Math.ceil((cycle + 1) / 40 - 1);
  const cycleNumber = cycle % 40;

  if ([register - 1, register, register + 1].includes(cycleNumber)) {
    drawing[row].push("█");
  } else drawing[row].push(" ");

  /* Part 2 end */

  if (instruction === "noop" && currentInstruction === 0) {
    cycle += 1;
  } else {
    if (currentInstruction !== 0) {
      register += currentInstruction;
      currentInstruction = 0;
    } else {
      currentInstruction = parseInt(value);
      i--;
    }
    cycle += 1;
  }

  if (breakPoints.includes(cycle + 1)) {
    signalStrengthSum += register * (cycle + 1);
  }
}

console.log(signalStrengthSum);

const imageRendered = drawing.map((e) => e.join("")).join("\n");

console.log(imageRendered);
