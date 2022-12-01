// Input

const text = await Deno.readTextFile("./input.txt");

// Part 1

const blocks = text.split("\n\n");

const calories = Array(blocks.length).fill(0) as number[];

for (let i = 0; i < calories.length; i++) {
  const blockCalories = blocks[i]
    .split("\n")
    .map((e) => parseInt(e))
    .reduce((a, b) => a + b, 0);

  calories[i] = blockCalories;
}

const maxCalories = Math.max(...calories);

console.log(maxCalories);

// Part 2

const topThreeCalories = calories
  .sort((a, b) => b - a)
  .slice(0, 3)
  .reduce((a, b) => a + b, 0);

console.log(topThreeCalories);
