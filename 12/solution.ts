// Input

const text = (await Deno.readTextFile("./input.txt")).split("\n");

const input = text.map((e) => e.split(""));

// Part 1

type Node = {
  x: number;
  y: number;
};

type QueueNode = {
  count: number;
  node: Node;
};

let start: Node | undefined;
let end: Node | undefined;

for (let i = 0; i < input.length; i++) {
  for (let j = 0; j < input[i].length; j++) {
    const char = input[i][j];
    if (char === "S") {
      start = { x: i, y: j };
      input[i][j] = "a";
    } else if (char === "E") {
      end = { x: i, y: j };
      input[i][j] = "z";
    }
  }
}

const queue: QueueNode[] = [{ count: 0, node: start! }];

const visited: Node[] = [];

let solution = 0;

while (queue.length > 0) {
  const { count, node } = queue.shift()!;
  const { x: i, y: j } = node;

  const positions: Node[] = [
    { x: i + 1, y: j },
    { x: i - 1, y: j },
    { x: i, y: j + 1 },
    { x: i, y: j - 1 },
  ];

  for (const node of positions) {
    if (
      node.x < 0 ||
      node.y < 0 ||
      node.x >= input.length ||
      node.y >= input[0].length
    )
      continue;

    if (solution !== 0) continue;

    if (visited.some((e) => e.x === node.x && e.y === node.y)) continue;

    if (input[node.x][node.y].charCodeAt(0) - input[i][j].charCodeAt(0) > 1)
      continue;

    if (end?.x === node.x && end.y === node.y) {
      solution = count + 1;
      break;
    }

    queue.push({ count: count + 1, node: { x: node.x, y: node.y } });

    visited.push({ x: node.x, y: node.y });
  }
}

console.log(solution);

// Part 2

queue.length = 0;

queue.push({ count: 0, node: end! });

visited.length = 0;

solution = 0;

while (queue.length > 0) {
  const { count, node } = queue.shift()!;
  const { x: i, y: j } = node;

  const positions: Node[] = [
    { x: i + 1, y: j },
    { x: i - 1, y: j },
    { x: i, y: j + 1 },
    { x: i, y: j - 1 },
  ];

  for (const node of positions) {
    if (
      node.x < 0 ||
      node.y < 0 ||
      node.x >= input.length ||
      node.y >= input[0].length
    )
      continue;

    if (solution !== 0) continue;

    if (visited.some((e) => e.x === node.x && e.y === node.y)) continue;

    if (input[node.x][node.y].charCodeAt(0) - input[i][j].charCodeAt(0) < -1)
      continue;

    if (input[node.x][node.y] === "a") {
      solution = count + 1;
      break;
    }

    queue.push({ count: count + 1, node: { x: node.x, y: node.y } });

    visited.push({ x: node.x, y: node.y });
  }
}

console.log(solution);
