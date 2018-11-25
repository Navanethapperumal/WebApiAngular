"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var MovieInformation = /** @class */ (function () {
    function MovieInformation() {
    }
    return MovieInformation;
}());
exports.MovieInformation = MovieInformation;
var Person = /** @class */ (function () {
    function Person() {
    }
    return Person;
}());
exports.Person = Person;
var MovieActor = /** @class */ (function (_super) {
    __extends(MovieActor, _super);
    function MovieActor() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return MovieActor;
}(Person));
exports.MovieActor = MovieActor;
var MovieProducer = /** @class */ (function (_super) {
    __extends(MovieProducer, _super);
    function MovieProducer() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return MovieProducer;
}(Person));
exports.MovieProducer = MovieProducer;
//# sourceMappingURL=moviemodel.js.map