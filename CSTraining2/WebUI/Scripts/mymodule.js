 function myModule() {
    this.name = 'module name';
    this.printStr = function (c, d) {
        console.log(c + d);
    };
 }
 module.exports = myModule;