﻿!function(e, t) {
    "object" == typeof exports && "undefined" != typeof module ? module.exports = t() : "function" == typeof define && define.amd ? define(t) : (e = e || self).createFileList = t()
}(this, (function() {
        "use strict";
        var e = function() {
            return new DataTransfer
        }
            , t = Array.prototype.concat;
        try {
            e()
        } catch (t) {
            e = function() {
                return new ClipboardEvent("").clipboardData
            }
        }
        return function() {
            for (var n = t.apply([], arguments), r = 0, o = n.length, i = e(); r < o; r++)
                i.items.add(n[r]);
            return i.files
        }
    }
));
