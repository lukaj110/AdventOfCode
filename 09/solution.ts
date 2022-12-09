// Input

const text = await Deno.readTextFile("./input.txt");

const input = text.split("\n");

// Part 1

type Position = {
  x: number;
  y: number;
};

function getDistanceFromHead(
  tailPosition: Position,
  headPosition: Position
): number {
  return Math.sqrt(
    Math.pow(headPosition.x - tailPosition.x, 2) +
      Math.pow(headPosition.y - tailPosition.y, 2)
  );
}

function isHeadInRange(
  tailPosition: Position,
  headPosition: Position
): boolean {
  return getDistanceFromHead(tailPosition, headPosition) <= 1.42;
}

function getPossiblePositions(tailPosition: Position) {
  const positions: Position[] = [];

  // Left
  positions.push({ x: tailPosition.x - 1, y: tailPosition.y });
  // Right
  positions.push({ x: tailPosition.x + 1, y: tailPosition.y });

  // Up
  positions.push({ x: tailPosition.x, y: tailPosition.y + 1 });
  // Down
  positions.push({ x: tailPosition.x, y: tailPosition.y - 1 });

  // Top-Left
  positions.push({ x: tailPosition.x - 1, y: tailPosition.y + 1 });
  // Top-Right
  positions.push({ x: tailPosition.x + 1, y: tailPosition.y + 1 });

  // Bottom-Left
  positions.push({ x: tailPosition.x - 1, y: tailPosition.y - 1 });
  // Bottom-Right
  positions.push({ x: tailPosition.x + 1, y: tailPosition.y - 1 });

  return positions;
}

function getBestPosition(
  tailPosition: Position,
  headPosition: Position
): Position {
  return getPossiblePositions(tailPosition)
    .filter((e) => isHeadInRange(e, headPosition))
    .sort(
      (a, b) =>
        getDistanceFromHead(a, headPosition) -
        getDistanceFromHead(b, headPosition)
    )[0];
}

function movedHeadPosition(direction: string, position: Position): Position {
  switch (direction) {
    case "L":
      return getPossiblePositions(position)[0];
    case "R":
      return getPossiblePositions(position)[1];
    case "U":
      return getPossiblePositions(position)[2];
    default:
      return getPossiblePositions(position)[3];
  }
}

const positions: Position[] = [{ x: 0, y: 0 }];

let headPosition: Position = { x: 0, y: 0 };
let tailPosition: Position = { x: 0, y: 0 };

for (const line of input) {
  const [direction, count] = line.split(" ");
  for (let i = 0; i < parseInt(count); i++) {
    headPosition = movedHeadPosition(direction, headPosition);

    if (isHeadInRange(tailPosition, headPosition)) continue;

    tailPosition = getBestPosition(tailPosition, headPosition);

    if (
      !positions.some((e) => e.x === tailPosition.x && e.y === tailPosition.y)
    )
      positions.push(tailPosition);
  }
}

console.log(positions.length);

// Part 2

const rope: Position[] = Array.from({ length: 10 }, (_, _i) => {
  return { x: 0, y: 0 };
});

const tailPositions: Position[] = [];

for (const line of input) {
  const [direction, count] = line.split(" ");
  for (let i = 0; i < parseInt(count); i++) {
    rope[0] = movedHeadPosition(direction, rope[0]);

    for (let j = 1; j < rope.length; j++) {
      if (isHeadInRange(rope[j], rope[j - 1])) continue;

      rope[j] = getBestPosition(rope[j], rope[j - 1]);
    }

    if (
      !tailPositions.some(
        (e) =>
          e.x === rope[rope.length - 1].x && e.y === rope[rope.length - 1].y
      )
    )
      tailPositions.push(rope[rope.length - 1]);
  }
}

console.log(tailPositions.length);
