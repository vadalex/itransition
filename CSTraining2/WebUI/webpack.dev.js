var webpack = require('webpack');
var cleanWebpackPlugin = require('clean-webpack-plugin');

module.exports = {
    entry: { todo: "./scripts/todoApp.js", dashboard: "./scripts/dashboardApp.js" },
    devtool: "source-map",    
    output: {
        filename: "scripts/bundles/[name].bundle.js",        
    },
    module: {
        rules: [
          {
              test: /\.css$/,
              use: ['style-loader', {
                  loader: 'css-loader',
                  options: { sourceMap: true }
              }]
          },
          {
              test: /\.html$/,
              use: [{
                  loader: 'html-loader',
                  options: {
                      minimize: true
                  }
              }]
          },
          {
              test: /\.(png|jpg|gif|svg|eot|ttf|woff|woff2)$/,
              loader: 'url-loader',
              options: {
                  limit: 10000,
                  publicPath: "../",
                  name: "./home/[hash].[ext]"
              }
          }
        ]
    },
    plugins: [       
        new cleanWebpackPlugin(
            [
                "./scripts/bundles",
                "./home"
            ]
        ),
        new webpack.ProvidePlugin({
            $: "jquery",
            jQuery: "jquery"
        })    
    ]
}