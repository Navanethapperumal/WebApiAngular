"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Gender;
(function (Gender) {
    Gender[Gender["Female"] = 0] = "Female";
    Gender[Gender["Male"] = 1] = "Male";
})(Gender = exports.Gender || (exports.Gender = {}));
(function (Gender) {
    function values() {
        return Object.keys(Gender).filter(function (type) { return isNaN(type) && type !== 'values'; });
    }
    Gender.values = values;
})(Gender = exports.Gender || (exports.Gender = {}));
//# sourceMappingURL=gender.enum.js.map