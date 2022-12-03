// Input

const text = await Deno.readTextFile("./input.txt");
const lines = text.split("\n");

// Part 1

const alphabet = String.fromCharCode(
  ...[
    ...Array.from({ length: 26 }, (_, i) => i + 97),
    ...Array.from({ length: 26 }, (_, i) => i + 65),
  ]
);

let prioritiesSum = 0;

for (const line of lines) {
  const firstHalf = line.substring(0, line.length / 2);
  const secondHalf = line.substring(line.length / 2);

  for (const char of firstHalf) {
    if (secondHalf.includes(char)) {
      prioritiesSum += alphabet.indexOf(char) + 1;
      break;
    }
  }
}

console.log(prioritiesSum);

// Part 2

let groupPrioritiesSum = 0;

for (let i = 0; i < lines.length; i += 3) {
  const commonChar = Array.from(lines[i]).find(
    (char) => lines[i + 1].includes(char) && lines[i + 2].includes(char)
  );

  if (commonChar) groupPrioritiesSum += alphabet.indexOf(commonChar) + 1;
}

console.log(groupPrioritiesSum);
