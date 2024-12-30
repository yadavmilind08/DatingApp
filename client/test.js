function deepestValley(arr) {
  let deepestValleySum = 0; // 10
  //   let currentValleySum = 0; // 0

  let isValley = false; // true

  let valleyStart = null;
  let valleyMin = null;

  for (let i = 1; i < arr.length; i++) {
    // decending
    if (arr[i] < arr[i - 1]) {
      // -2 < -1
      if (!isValley) {
        isValley = true;
        valleyStart = arr[i - 1]; // 7
        valleyMin = arr[i]; // -2
        // currentValleySum = Math.abs(arr[i - 1]); // 7
      } else {
        valleyMin = Math.min(valleyMin, arr[i]); // -2
      }
      // currentValleySum += Math.abs(arr[i]); // 10
    } else if (arr[i] > arr[i - 1]) {
      // ascending
      if (isValley) {
        const depth = valleyStart - valleyMin;
        deepestValleySum = Math.max(deepestValleySum, depth);
        // currentValleySum = 0;
        isValley = false;
        valleyStart = null;
        valleyMin = null;
      }
    }
  }

  //   if (isValley) {
  //     deepestValleySum = Math.max(deepestValleySum, currentValleySum);
  //   }

  console.log(deepestValleySum);
  return deepestValleySum; // 10
}

deepestValley([4, 7, -1, -2, 0, 3, 1, 3, 7, -1]);
