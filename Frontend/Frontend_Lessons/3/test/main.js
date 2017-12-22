function Shape(width, height) {
    this.width = width;
    this.height = height;
}

Shape.prototype.style = 'background: cyan;';
Shape.prototype.move = function (x, y) {
    this.x = x;
    this.y = y;
    return this;
}

function Circle(size) {
    this.height = size;
    this.width = size;
    this.style = this.style.concat(' border-radius: 50%;');
}

Circle.prototype = Object.create(Shape.prototype);

var circle = new Circle(12);
console.log(circle.move(10,10));


// сделать квадрат круг и прямоугольник, которые смогут перемещаться. сделать draganddrop (брать элемент и перемещать его)
// сделать список в котором можно выбрать эти фигуры