// console.log("1");
// console.log("2");
// console.log("3");
// console.log("4");
//
// let resolver = (resolve, reject) => {
//     setTimeout(() => {
//         console.log("5")
//         reject();
//     }, 1000);
// }
//
// let print5Promise = new Promise(resolver);
// print5Promise.then(() => {
//     console.log("6")
// }).catch((err) => {
//         consol.error(err);
// });
//
// async function print56() {
//     await print5Promise;
//     console.log("6");
// }
//
// async function main() {
//     print56();
// }
//
//
// main();