// Input

const text = await Deno.readTextFile("./input.txt");

// Part 1

let startOfMessageCount = 0;

for (let i = 0; i < text.length - 3; i++) {
  const stringPartition = text.substring(i, i + 4);
  if (new Set(stringPartition).size === stringPartition.length) {
    startOfMessageCount = i + 4;

    break;
  }
}

console.log(startOfMessageCount);

// Part 2

startOfMessageCount = 0;

for (let i = 0; i < text.length - 13; i++) {
  const stringPartition = text.substring(i, i + 14);
  if (new Set(stringPartition).size === stringPartition.length) {
    startOfMessageCount = i + 14;
    break;
  }
}

console.log(startOfMessageCount);
