
str = 'fsdfsd dfdsgf fdgdfg gfdgdfg hgdhfgh'
function compose() {
    var funcs = Array.prototype.slice.call(arguments);

    return function getResult(startString) {
        return funcs.reduce(function(result, nextFunc) {
            return nextFunc(result);
        }, startString)
    }
}

var upperCase = function(str){
    return str.toUpperCase();
}

var getLength = function(str){
    return (str + '_' + str.length);
}

var sayHello = function( greet){
    return function(name){
        return 'Hello ' + greet + ' ' + name;
    }
}


var greetingMr = compose(upperCase, sayHello('Mister'));

var greetingDearWithLength = compose(getLength, sayHello('Dear'));

console.log(str.split(' ').map(greetingMr).join('\n'));
console.log(str.split(' ').map(greetingDearWithLength).join('\n'));

//.reduce()!!!!!

