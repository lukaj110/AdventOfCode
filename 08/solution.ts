// Input

const text = await Deno.readTextFile("./input.txt");

const input = text.split("\n");

// Part 1

const treePatch: number[][] = [];

for (const line of input) {
  treePatch.push([...line.split("").map((e) => parseInt(e))]);
}

let visibleTreesCount = 0;

visibleTreesCount += 2 * treePatch.length + 2 * treePatch[0].length - 4;

for (let i = 1; i < treePatch.length - 1; i++) {
  for (let j = 1; j < treePatch[i].length - 1; j++) {
    const treeHeight = treePatch[i][j];

    let [visibleLeft, visibleRight, visibleTop, visibleBottom] = [
      true,
      true,
      true,
      true,
    ];

    for (let k = 0; k < treePatch[i].length; k++) {
      // Left
      if (k < j && treePatch[i][k] >= treeHeight) visibleLeft = false;

      // Right
      if (k > j && treePatch[i][k] >= treeHeight) visibleRight = false;

      // Top
      if (k < i && treePatch[k][j] >= treeHeight) visibleTop = false;

      // Bottom
      if (k > i && treePatch[k][j] >= treeHeight) visibleBottom = false;
    }

    if ([visibleLeft, visibleRight, visibleTop, visibleBottom].some((e) => e))
      visibleTreesCount++;
  }
}

console.log(visibleTreesCount);

// Part 2

let highestScenicScore = 0;

for (let i = 1; i < treePatch.length - 1; i++) {
  for (let j = 1; j < treePatch[i].length - 1; j++) {
    const treeHeight = treePatch[i][j];

    let [visibleLeft, visibleRight, visibleTop, visibleBottom] = [
      true,
      true,
      true,
      true,
    ];

    let [
      visibleLeftCount,
      visibleRightCount,
      visibleTopCount,
      visibleBottomCount,
    ] = [0, 0, 0, 0];

    for (let k = 1; k < treePatch[i].length; k++) {
      // Left
      if (j - k >= 0) {
        if (visibleLeft) {
          visibleLeftCount++;
          if (treePatch[i][j - k] >= treeHeight) visibleLeft = false;
        }
      }

      // Right
      if (j + k < treePatch[i].length) {
        if (visibleRight) {
          visibleRightCount++;
          if (treePatch[i][j + k] >= treeHeight) visibleRight = false;
        }
      }

      // Top
      if (i - k >= 0) {
        if (visibleTop) {
          visibleTopCount++;
          if (treePatch[i - k][j] >= treeHeight) visibleTop = false;
        }
      }
      // Bottom
      if (i + k < treePatch.length) {
        if (visibleBottom) {
          visibleBottomCount++;
          if (treePatch[i + k][j] >= treeHeight) visibleBottom = false;
        }
      }
    }

    const sum = [
      visibleTopCount,
      visibleLeftCount,
      visibleRightCount,
      visibleBottomCount,
    ].reduce((a, b) => a * b);

    if (sum > highestScenicScore) highestScenicScore = sum;
  }
}

console.log(highestScenicScore);
