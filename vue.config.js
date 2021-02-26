module.exports = {
  publicPath: "./",
  outputDir: "../wwwroot",
  filenameHashing: true,
  pages: {
    index: {
      entry: "src/main.js",
      template: "public/index.html",
      filename: "index.html",
      chunks: ["chunk-vendors", "chunk-common", "index"]
    },
    result: {
      entry: "src/result.js",
      template: "public/index.html",
      filename: "result.html",
      chunks: ["chunk-vendors", "chunk-common", "result"]
    }
  }
};
