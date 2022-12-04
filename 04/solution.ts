// Input

const text = await Deno.readTextFile("./input.txt");
const lines = text.split("\n");

// Part 1

let assigmentPairCount = 0;

for (const line of lines) {
  const pairs = line.split(",");

  const [firstPair, secondPair] = pairs.map((e) =>
    e.split("-").map((el) => parseInt(el))
  );

  if (firstPair[0] <= secondPair[0] && firstPair[1] >= secondPair[1])
    assigmentPairCount++;
  else if (secondPair[0] <= firstPair[0] && secondPair[1] >= firstPair[1])
    assigmentPairCount++;
}

console.log(assigmentPairCount);

// Part 2

let anyOverlapCount = 0;

for (const line of lines) {
  const pairs = line.split(",");

  const [firstPair, secondPair] = pairs.map((e) =>
    e.split("-").map((el) => parseInt(el))
  );

  const firstPairRange = Array.from(
    { length: firstPair[1] - firstPair[0] + 1 },
    (_, i) => i + firstPair[0]
  );

  const secondPairRange = Array.from(
    { length: secondPair[1] - secondPair[0] + 1 },
    (_, i) => i + secondPair[0]
  );

  if (firstPairRange.some((e) => secondPairRange.includes(e)))
    anyOverlapCount++;
}

console.log(anyOverlapCount);
