(window),
    function (e) {
        "object" == typeof module && "object" == typeof module.exports ? module.exports = e() :
        "function" == typeof define && define.amd ? define(e) : e()
    }
    (function () {
        return function (e, t, n, i) {
            function r(e) {
                for (var t = -1, n = e ? e.length : 0, i = []; ++t < n;) {
                    var r = e[t];
                    r && i.push(r)
                }
                return i
            }

            function a(e) {
                return g.isWrapped(e) ? e = [].slice.call(e) : g.isNode(e) && (e = [e]), e
            }

            function o(e) {
                var t = p.data(e, "velocity");
                return null === t ? i : t
            }

            function s(e) {
                return function (t) {
                    return Math.round(t * e) * (1 / e)
                }
            }

            function l(e, n, i, r) {
                function a(e, t) {
                    return 1 - 3 * t + 3 * e
                }

                function o(e, t) {
                    return 3 * t - 6 * e
                }

                function s(e) {
                    return 3 * e
                }

                function l(e, t, n) {
                    return ((a(t, n) * e + o(t, n)) * e + s(t)) * e
                }

                function u(e, t, n) {
                    return 3 * a(t, n) * e * e + 2 * o(t, n) * e + s(t)
                }

                function c(t, n) {
                    for (var r = 0; g > r; ++r) {
                        var a = u(n, e, i);
                        if (0 === a) return n;
                        var o = l(n, e, i) - t;
                        n -= o / a
                    }
                    return n
                }

                function d() {
                    for (var t = 0; b > t; ++t) C[t] = l(t * w, e, i)
                }

                function p(t, n, r) {
                    var a, o, s = 0;
                    do o = n + (r - n) / 2, a = l(o, e, i) - t, a > 0 ? r = o : n = o; while (Math.abs(a) > m && ++s < y);
                    return o
                }

                function h(t) {
                    for (var n = 0, r = 1, a = b - 1; r != a && C[r] <= t; ++r) n += w;
                    --r;
                    var o = (t - C[r]) / (C[r + 1] - C[r]),
                        s = n + o * w,
                        l = u(s, e, i);
                    return l >= v ? c(t, s) : 0 == l ? s : p(t, n, n + w)
                }

                function f() {
                    S = !0, (e != n || i != r) && d()
                }
                var g = 4,
                    v = .001,
                    m = 1e-7,
                    y = 10,
                    b = 11,
                    w = 1 / (b - 1),
                    x = "Float32Array" in t;
                if (4 !== arguments.length) return !1;
                for (var k = 0; 4 > k; ++k)
                    if ("number" != typeof arguments[k] || isNaN(arguments[k]) || !isFinite(arguments[k])) return !1;
                e = Math.min(e, 1), i = Math.min(i, 1), e = Math.max(e, 0), i = Math.max(i, 0);
                var C = x ? new Float32Array(b) : new Array(b),
                    S = !1,
                    T = function (t) {
                        return S || f(), e === n && i === r ? t : 0 === t ? 0 : 1 === t ? 1 : l(h(t), n, r)
                    };
                T.getControlPoints = function () {
                    return [{
                        x: e,
                        y: n
                    }, {
                        x: i,
                        y: r
                    }]
                };
                var P = "generateBezier(" + [e, n, i, r] + ")";
                return T.toString = function () {
                    return P
                }, T
            }

            function u(e, t) {
                var n = e;
                return g.isString(e) ? b.Easings[e] || (n = !1) : n = g.isArray(e) && 1 === e.length ? s.apply(null, e) : g.isArray(e) && 2 === e.length ? w.apply(null, e.concat([t])) : g.isArray(e) && 4 === e.length ? l.apply(null, e) : !1, n === !1 && (n = b.Easings[b.defaults.easing] ? b.defaults.easing : y), n
            }

            function c(e) {
                if (e) {
                    var t = (new Date).getTime(),
                        n = b.State.calls.length;
                    n > 1e4 && (b.State.calls = r(b.State.calls));
                    for (var a = 0; n > a; a++)
                        if (b.State.calls[a]) {
                            var s = b.State.calls[a],
                                l = s[0],
                                u = s[2],
                                h = s[3],
                                f = !!h,
                                v = null;
                            h || (h = b.State.calls[a][3] = t - 16);
                            for (var m = Math.min((t - h) / u.duration, 1), y = 0, w = l.length; w > y; y++) {
                                var k = l[y],
                                    S = k.element;
                                if (o(S)) {
                                    var T = !1;
                                    if (u.display !== i && null !== u.display && "none" !== u.display) {
                                        if ("flex" === u.display) {
                                            var P = ["-webkit-box", "-moz-box", "-ms-flexbox", "-webkit-flex"];
                                            p.each(P, function (e, t) {
                                                x.setPropertyValue(S, "display", t)
                                            })
                                        }
                                        x.setPropertyValue(S, "display", u.display)
                                    }
                                    u.visibility !== i && "hidden" !== u.visibility && x.setPropertyValue(S, "visibility", u.visibility);
                                    for (var O in k)
                                        if ("element" !== O) {
                                            var E, A = k[O],
                                                M = g.isString(A.easing) ? b.Easings[A.easing] : A.easing;
                                            if (1 === m) E = A.endValue;
                                            else {
                                                var V = A.endValue - A.startValue;
                                                if (E = A.startValue + V * M(m, u, V), !f && E === A.currentValue) continue
                                            }
                                            if (A.currentValue = E, "tween" === O) v = E;
                                            else {
                                                if (x.Hooks.registered[O]) {
                                                    var _ = x.Hooks.getRoot(O),
                                                        I = o(S).rootPropertyValueCache[_];
                                                    I && (A.rootPropertyValue = I)
                                                }
                                                var j = x.setPropertyValue(S, O, A.currentValue + (0 === parseFloat(E) ? "" : A.unitType), A.rootPropertyValue, A.scrollData);
                                                x.Hooks.registered[O] && (o(S).rootPropertyValueCache[_] = x.Normalizations.registered[_] ? x.Normalizations.registered[_]("extract", null, j[1]) : j[1]), "transform" === j[0] && (T = !0)
                                            }
                                        }
                                    u.mobileHA && o(S).transformCache.translate3d === i && (o(S).transformCache.translate3d = "(0px, 0px, 0px)", T = !0), T && x.flushTransformCache(S)
                                }
                            }
                            u.display !== i && "none" !== u.display && (b.State.calls[a][2].display = !1), u.visibility !== i && "hidden" !== u.visibility && (b.State.calls[a][2].visibility = !1), u.progress && u.progress.call(s[1], s[1], m, Math.max(0, h + u.duration - t), h, v), 1 === m && d(a)
                        }
                }
                b.State.isTicking && C(c)
            }

            function d(e, t) {
                if (!b.State.calls[e]) return !1;
                for (var n = b.State.calls[e][0], r = b.State.calls[e][1], a = b.State.calls[e][2], s = b.State.calls[e][4], l = !1, u = 0, c = n.length; c > u; u++) {
                    var d = n[u].element;
                    if (t || a.loop || ("none" === a.display && x.setPropertyValue(d, "display", a.display), "hidden" === a.visibility && x.setPropertyValue(d, "visibility", a.visibility)), a.loop !== !0 && (p.queue(d)[1] === i || !/\.velocityQueueEntryFlag/i.test(p.queue(d)[1])) && o(d)) {
                        o(d).isAnimating = !1, o(d).rootPropertyValueCache = {};
                        var h = !1;
                        p.each(x.Lists.transforms3D, function (e, t) {
                            var n = /^scale/.test(t) ? 1 : 0,
                                r = o(d).transformCache[t];
                            o(d).transformCache[t] !== i && new RegExp("^\\(" + n + "[^.]").test(r) && (h = !0, delete o(d).transformCache[t])
                        }), a.mobileHA && (h = !0, delete o(d).transformCache.translate3d), h && x.flushTransformCache(d), x.Values.removeClass(d, "velocity-animating")
                    }
                    if (!t && a.complete && !a.loop && u === c - 1) try {
                        a.complete.call(r, r)
                    } catch (f) {
                        setTimeout(function () {
                            throw f
                        }, 1)
                    }
                    s && a.loop !== !0 && s(r), o(d) && a.loop === !0 && !t && (p.each(o(d).tweensContainer, function (e, t) {
                        /^rotate/.test(e) && 360 === parseFloat(t.endValue) && (t.endValue = 0, t.startValue = 360), /^backgroundPosition/.test(e) && 100 === parseFloat(t.endValue) && "%" === t.unitType && (t.endValue = 0, t.startValue = 100)
                    }), b(d, "reverse", {
                        loop: !0,
                        delay: a.delay
                    })), a.queue !== !1 && p.dequeue(d, a.queue)
                }
                b.State.calls[e] = !1;
                for (var g = 0, v = b.State.calls.length; v > g; g++)
                    if (b.State.calls[g] !== !1) {
                        l = !0;
                        break
                    }
                l === !1 && (b.State.isTicking = !1, delete b.State.calls, b.State.calls = [])
            }
            var p, h = function () {
                if (n.documentMode) return n.documentMode;
                for (var e = 7; e > 4; e--) {
                    var t = n.createElement("div");
                    if (t.innerHTML = "<!--[if IE " + e + "]><span></span><![endif]-->", t.getElementsByTagName("span").length) return t = null, e
                }
                return i
            }(),
                f = function () {
                    var e = 0;
                    return t.webkitRequestAnimationFrame || t.mozRequestAnimationFrame || function (t) {
                        var n, i = (new Date).getTime();
                        return n = Math.max(0, 16 - (i - e)), e = i + n, setTimeout(function () {
                            t(i + n)
                        }, n)
                    }
                }(),
                g = {
                    isString: function (e) {
                        return "string" == typeof e
                    },
                    isArray: Array.isArray || function (e) {
                        return "[object Array]" === Object.prototype.toString.call(e)
                    },
                    isFunction: function (e) {
                        return "[object Function]" === Object.prototype.toString.call(e)
                    },
                    isNode: function (e) {
                        return e && e.nodeType
                    },
                    isNodeList: function (e) {
                        return "object" == typeof e && /^\[object (HTMLCollection|NodeList|Object)\]$/.test(Object.prototype.toString.call(e)) && e.length !== i && (0 === e.length || "object" == typeof e[0] && e[0].nodeType > 0)
                    },
                    isWrapped: function (e) {
                        return e && (e.jquery || t.Zepto && t.Zepto.zepto.isZ(e))
                    },
                    isSVG: function (e) {
                        return t.SVGElement && e instanceof t.SVGElement
                    },
                    isEmptyObject: function (e) {
                        for (var t in e) return !1;
                        return !0
                    }
                },
                v = !1;
            if (e.fn && e.fn.jquery ? (p = e, v = !0) : p = t.Velocity.Utilities, 8 >= h && !v) throw new Error("Velocity: IE8 and below require jQuery to be loaded before Velocity.");
            if (7 >= h) return void (jQuery.fn.velocity = jQuery.fn.animate);
            var m = 400,
                y = "swing",
                b = {
                    State: {
                        isMobile: /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent),
                        isAndroid: /Android/i.test(navigator.userAgent),
                        isGingerbread: /Android 2\.3\.[3-7]/i.test(navigator.userAgent),
                        isChrome: t.chrome,
                        isFirefox: /Firefox/i.test(navigator.userAgent),
                        prefixElement: n.createElement("div"),
                        prefixMatches: {},
                        scrollAnchor: null,
                        scrollPropertyLeft: null,
                        scrollPropertyTop: null,
                        isTicking: !1,
                        calls: []
                    },
                    CSS: {},
                    Utilities: p,
                    Redirects: {},
                    Easings: {},
                    Promise: t.Promise,
                    defaults: {
                        queue: "",
                        duration: m,
                        easing: y,
                        begin: i,
                        complete: i,
                        progress: i,
                        display: i,
                        visibility: i,
                        loop: !1,
                        delay: !1,
                        mobileHA: !0,
                        _cacheValues: !0
                    },
                    init: function (e) {
                        p.data(e, "velocity", {
                            isSVG: g.isSVG(e),
                            isAnimating: !1,
                            computedStyle: null,
                            tweensContainer: null,
                            rootPropertyValueCache: {},
                            transformCache: {}
                        })
                    },
                    hook: null,
                    mock: !1,
                    version: {
                        major: 1,
                        minor: 2,
                        patch: 2
                    },
                    debug: !1
                };
            t.pageYOffset !== i ? (b.State.scrollAnchor = t, b.State.scrollPropertyLeft = "pageXOffset", b.State.scrollPropertyTop = "pageYOffset") : (b.State.scrollAnchor = n.documentElement || n.body.parentNode || n.body, b.State.scrollPropertyLeft = "scrollLeft", b.State.scrollPropertyTop = "scrollTop");
            var w = function () {
                function e(e) {
                    return -e.tension * e.x - e.friction * e.v
                }

                function t(t, n, i) {
                    var r = {
                        x: t.x + i.dx * n,
                        v: t.v + i.dv * n,
                        tension: t.tension,
                        friction: t.friction
                    };
                    return {
                        dx: r.v,
                        dv: e(r)
                    }
                }

                function n(n, i) {
                    var r = {
                        dx: n.v,
                        dv: e(n)
                    },
                        a = t(n, .5 * i, r),
                        o = t(n, .5 * i, a),
                        s = t(n, i, o),
                        l = 1 / 6 * (r.dx + 2 * (a.dx + o.dx) + s.dx),
                        u = 1 / 6 * (r.dv + 2 * (a.dv + o.dv) + s.dv);
                    return n.x = n.x + l * i, n.v = n.v + u * i, n
                }
                return function i(e, t, r) {
                    var a, o, s, l = {
                        x: -1,
                        v: 0,
                        tension: null,
                        friction: null
                    },
                        u = [0],
                        c = 0,
                        d = 1e-4,
                        p = .016;
                    for (e = parseFloat(e) || 500, t = parseFloat(t) || 20, r = r || null, l.tension = e, l.friction = t, a = null !== r, a ? (c = i(e, t), o = c / r * p) : o = p; s = n(s || l, o), u.push(1 + s.x), c += 16, Math.abs(s.x) > d && Math.abs(s.v) > d;);
                    return a ? function (e) {
                        return u[e * (u.length - 1) | 0]
                    } : c
                }
            }();
            b.Easings = {
                linear: function (e) {
                    return e
                },
                swing: function (e) {
                    return .5 - Math.cos(e * Math.PI) / 2
                },
                spring: function (e) {
                    return 1 - Math.cos(4.5 * e * Math.PI) * Math.exp(6 * -e)
                }
            }, p.each([
                ["ease", [.25, .1, .25, 1]],
                ["ease-in", [.42, 0, 1, 1]],
                ["ease-out", [0, 0, .58, 1]],
                ["ease-in-out", [.42, 0, .58, 1]],
                ["easeInSine", [.47, 0, .745, .715]],
                ["easeOutSine", [.39, .575, .565, 1]],
                ["easeInOutSine", [.445, .05, .55, .95]],
                ["easeInQuad", [.55, .085, .68, .53]],
                ["easeOutQuad", [.25, .46, .45, .94]],
                ["easeInOutQuad", [.455, .03, .515, .955]],
                ["easeInCubic", [.55, .055, .675, .19]],
                ["easeOutCubic", [.215, .61, .355, 1]],
                ["easeInOutCubic", [.645, .045, .355, 1]],
                ["easeInQuart", [.895, .03, .685, .22]],
                ["easeOutQuart", [.165, .84, .44, 1]],
                ["easeInOutQuart", [.77, 0, .175, 1]],
                ["easeInQuint", [.755, .05, .855, .06]],
                ["easeOutQuint", [.23, 1, .32, 1]],
                ["easeInOutQuint", [.86, 0, .07, 1]],
                ["easeInExpo", [.95, .05, .795, .035]],
                ["easeOutExpo", [.19, 1, .22, 1]],
                ["easeInOutExpo", [1, 0, 0, 1]],
                ["easeInCirc", [.6, .04, .98, .335]],
                ["easeOutCirc", [.075, .82, .165, 1]],
                ["easeInOutCirc", [.785, .135, .15, .86]]
            ], function (e, t) {
                b.Easings[t[0]] = l.apply(null, t[1])
            });
            var x = b.CSS = {
                RegEx: {
                    isHex: /^#([A-f\d]{3}){1,2}$/i,
                    valueUnwrap: /^[A-z]+\((.*)\)$/i,
                    wrappedValueAlreadyExtracted: /[0-9.]+ [0-9.]+ [0-9.]+( [0-9.]+)?/,
                    valueSplit: /([A-z]+\(.+\))|(([A-z0-9#-.]+?)(?=\s|$))/gi
                },
                Lists: {
                    colors: ["fill", "stroke", "stopColor", "color", "backgroundColor", "borderColor", "borderTopColor", "borderRightColor", "borderBottomColor", "borderLeftColor", "outlineColor"],
                    transformsBase: ["translateX", "translateY", "scale", "scaleX", "scaleY", "skewX", "skewY", "rotateZ"],
                    transforms3D: ["transformPerspective", "translateZ", "scaleZ", "rotateX", "rotateY"]
                },
                Hooks: {
                    templates: {
                        textShadow: ["Color X Y Blur", "black 0px 0px 0px"],
                        boxShadow: ["Color X Y Blur Spread", "black 0px 0px 0px 0px"],
                        clip: ["Top Right Bottom Left", "0px 0px 0px 0px"],
                        backgroundPosition: ["X Y", "0% 0%"],
                        transformOrigin: ["X Y Z", "50% 50% 0px"],
                        perspectiveOrigin: ["X Y", "50% 50%"]
                    },
                    registered: {},
                    register: function () {
                        for (var e = 0; e < x.Lists.colors.length; e++) {
                            var t = "color" === x.Lists.colors[e] ? "0 0 0 1" : "255 255 255 1";
                            x.Hooks.templates[x.Lists.colors[e]] = ["Red Green Blue Alpha", t]
                        }
                        var n, i, r;
                        if (h)
                            for (n in x.Hooks.templates) {
                                i = x.Hooks.templates[n], r = i[0].split(" ");
                                var a = i[1].match(x.RegEx.valueSplit);
                                "Color" === r[0] && (r.push(r.shift()), a.push(a.shift()), x.Hooks.templates[n] = [r.join(" "), a.join(" ")])
                            }
                        for (n in x.Hooks.templates) {
                            i = x.Hooks.templates[n], r = i[0].split(" ");
                            for (var e in r) {
                                var o = n + r[e],
                                    s = e;
                                x.Hooks.registered[o] = [n, s]
                            }
                        }
                    },
                    getRoot: function (e) {
                        var t = x.Hooks.registered[e];
                        return t ? t[0] : e
                    },
                    cleanRootPropertyValue: function (e, t) {
                        return x.RegEx.valueUnwrap.test(t) && (t = t.match(x.RegEx.valueUnwrap)[1]), x.Values.isCSSNullValue(t) && (t = x.Hooks.templates[e][1]), t
                    },
                    extractValue: function (e, t) {
                        var n = x.Hooks.registered[e];
                        if (n) {
                            var i = n[0],
                                r = n[1];
                            return t = x.Hooks.cleanRootPropertyValue(i, t), t.toString().match(x.RegEx.valueSplit)[r]
                        }
                        return t
                    },
                    injectValue: function (e, t, n) {
                        var i = x.Hooks.registered[e];
                        if (i) {
                            var r, a, o = i[0],
                                s = i[1];
                            return n = x.Hooks.cleanRootPropertyValue(o, n), r = n.toString().match(x.RegEx.valueSplit), r[s] = t, a = r.join(" ")
                        }
                        return n
                    }
                },
                Normalizations: {
                    registered: {
                        clip: function (e, t, n) {
                            switch (e) {
                                case "name":
                                    return "clip";
                                case "extract":
                                    var i;
                                    return x.RegEx.wrappedValueAlreadyExtracted.test(n) ? i = n : (i = n.toString().match(x.RegEx.valueUnwrap), i = i ? i[1].replace(/,(\s+)?/g, " ") : n), i;
                                case "inject":
                                    return "rect(" + n + ")"
                            }
                        },
                        blur: function (e, t, n) {
                            switch (e) {
                                case "name":
                                    return b.State.isFirefox ? "filter" : "-webkit-filter";
                                case "extract":
                                    var i = parseFloat(n);
                                    if (!i && 0 !== i) {
                                        var r = n.toString().match(/blur\(([0-9]+[A-z]+)\)/i);
                                        i = r ? r[1] : 0
                                    }
                                    return i;
                                case "inject":
                                    return parseFloat(n) ? "blur(" + n + ")" : "none"
                            }
                        },
                        opacity: function (e, t, n) {
                            if (8 >= h) switch (e) {
                                case "name":
                                    return "filter";
                                case "extract":
                                    var i = n.toString().match(/alpha\(opacity=(.*)\)/i);
                                    return n = i ? i[1] / 100 : 1;
                                case "inject":
                                    return t.style.zoom = 1, parseFloat(n) >= 1 ? "" : "alpha(opacity=" + parseInt(100 * parseFloat(n), 10) + ")"
                            } else switch (e) {
                                case "name":
                                    return "opacity";
                                case "extract":
                                    return n;
                                case "inject":
                                    return n
                            }
                        }
                    },
                    register: function () {
                        9 >= h || b.State.isGingerbread || (x.Lists.transformsBase = x.Lists.transformsBase.concat(x.Lists.transforms3D));
                        for (var e = 0; e < x.Lists.transformsBase.length; e++) ! function () {
                            var t = x.Lists.transformsBase[e];
                            x.Normalizations.registered[t] = function (e, n, r) {
                                switch (e) {
                                    case "name":
                                        return "transform";
                                    case "extract":
                                        return o(n) === i || o(n).transformCache[t] === i ? /^scale/i.test(t) ? 1 : 0 : o(n).transformCache[t].replace(/[()]/g, "");
                                    case "inject":
                                        var a = !1;
                                        switch (t.substr(0, t.length - 1)) {
                                            case "translate":
                                                a = !/(%|px|em|rem|vw|vh|\d)$/i.test(r);
                                                break;
                                            case "scal":
                                            case "scale":
                                                b.State.isAndroid && o(n).transformCache[t] === i && 1 > r && (r = 1), a = !/(\d)$/i.test(r);
                                                break;
                                            case "skew":
                                                a = !/(deg|\d)$/i.test(r);
                                                break;
                                            case "rotate":
                                                a = !/(deg|\d)$/i.test(r)
                                        }
                                        return a || (o(n).transformCache[t] = "(" + r + ")"), o(n).transformCache[t]
                                }
                            }
                        }();
                        for (var e = 0; e < x.Lists.colors.length; e++) ! function () {
                            var t = x.Lists.colors[e];
                            x.Normalizations.registered[t] = function (e, n, r) {
                                switch (e) {
                                    case "name":
                                        return t;
                                    case "extract":
                                        var a;
                                        if (x.RegEx.wrappedValueAlreadyExtracted.test(r)) a = r;
                                        else {
                                            var o, s = {
                                                black: "rgb(0, 0, 0)",
                                                blue: "rgb(0, 0, 255)",
                                                gray: "rgb(128, 128, 128)",
                                                green: "rgb(0, 128, 0)",
                                                red: "rgb(255, 0, 0)",
                                                white: "rgb(255, 255, 255)"
                                            };
                                            /^[A-z]+$/i.test(r) ? o = s[r] !== i ? s[r] : s.black : x.RegEx.isHex.test(r) ? o = "rgb(" + x.Values.hexToRgb(r).join(" ") + ")" : /^rgba?\(/i.test(r) || (o = s.black), a = (o || r).toString().match(x.RegEx.valueUnwrap)[1].replace(/,(\s+)?/g, " ")
                                        }
                                        return 8 >= h || 3 !== a.split(" ").length || (a += " 1"), a;
                                    case "inject":
                                        return 8 >= h ? 4 === r.split(" ").length && (r = r.split(/\s+/).slice(0, 3).join(" ")) : 3 === r.split(" ").length && (r += " 1"), (8 >= h ? "rgb" : "rgba") + "(" + r.replace(/\s+/g, ",").replace(/\.(\d)+(?=,)/g, "") + ")"
                                }
                            }
                        }()
                    }
                },
                Names: {
                    camelCase: function (e) {
                        return e.replace(/-(\w)/g, function (e, t) {
                            return t.toUpperCase()
                        })
                    },
                    SVGAttribute: function (e) {
                        var t = "width|height|x|y|cx|cy|r|rx|ry|x1|x2|y1|y2";
                        return (h || b.State.isAndroid && !b.State.isChrome) && (t += "|transform"), new RegExp("^(" + t + ")$", "i").test(e)
                    },
                    prefixCheck: function (e) {
                        if (b.State.prefixMatches[e]) return [b.State.prefixMatches[e], !0];
                        for (var t = ["", "Webkit", "Moz", "ms", "O"], n = 0, i = t.length; i > n; n++) {
                            var r;
                            if (r = 0 === n ? e : t[n] + e.replace(/^\w/, function (e) {
                                    return e.toUpperCase()
                            }), g.isString(b.State.prefixElement.style[r])) return b.State.prefixMatches[e] = r, [r, !0]
                        }
                        return [e, !1]
                    }
                },
                Values: {
                    hexToRgb: function (e) {
                        var t, n = /^#?([a-f\d])([a-f\d])([a-f\d])$/i,
                            i = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i;
                        return e = e.replace(n, function (e, t, n, i) {
                            return t + t + n + n + i + i
                        }), t = i.exec(e), t ? [parseInt(t[1], 16), parseInt(t[2], 16), parseInt(t[3], 16)] : [0, 0, 0]
                    },
                    isCSSNullValue: function (e) {
                        return 0 == e || /^(none|auto|transparent|(rgba\(0, ?0, ?0, ?0\)))$/i.test(e)
                    },
                    getUnitType: function (e) {
                        return /^(rotate|skew)/i.test(e) ? "deg" : /(^(scale|scaleX|scaleY|scaleZ|alpha|flexGrow|flexHeight|zIndex|fontWeight)$)|((opacity|red|green|blue|alpha)$)/i.test(e) ? "" : "px"
                    },
                    getDisplayType: function (e) {
                        var t = e && e.tagName.toString().toLowerCase();
                        return /^(b|big|i|small|tt|abbr|acronym|cite|code|dfn|em|kbd|strong|samp|var|a|bdo|br|img|map|object|q|script|span|sub|sup|button|input|label|select|textarea)$/i.test(t) ? "inline" : /^(li)$/i.test(t) ? "list-item" : /^(tr)$/i.test(t) ? "table-row" : /^(table)$/i.test(t) ? "table" : /^(tbody)$/i.test(t) ? "table-row-group" : "block"
                    },
                    addClass: function (e, t) {
                        e.classList ? e.classList.add(t) : e.className += (e.className.length ? " " : "") + t
                    },
                    removeClass: function (e, t) {
                        e.classList ? e.classList.remove(t) : e.className = e.className.toString().replace(new RegExp("(^|\\s)" + t.split(" ").join("|") + "(\\s|$)", "gi"), " ")
                    }
                },
                getPropertyValue: function (e, n, r, a) {
                    function s(e, n) {
                        function r() {
                            u && x.setPropertyValue(e, "display", "none")
                        }
                        var l = 0;
                        if (8 >= h) l = p.css(e, n);
                        else {
                            var u = !1;
                            if (/^(width|height)$/.test(n) && 0 === x.getPropertyValue(e, "display") && (u = !0, x.setPropertyValue(e, "display", x.Values.getDisplayType(e))), !a) {
                                if ("height" === n && "border-box" !== x.getPropertyValue(e, "boxSizing").toString().toLowerCase()) {
                                    var c = e.offsetHeight - (parseFloat(x.getPropertyValue(e, "borderTopWidth")) || 0) - (parseFloat(x.getPropertyValue(e, "borderBottomWidth")) || 0) - (parseFloat(x.getPropertyValue(e, "paddingTop")) || 0) - (parseFloat(x.getPropertyValue(e, "paddingBottom")) || 0);
                                    return r(), c
                                }
                                if ("width" === n && "border-box" !== x.getPropertyValue(e, "boxSizing").toString().toLowerCase()) {
                                    var d = e.offsetWidth - (parseFloat(x.getPropertyValue(e, "borderLeftWidth")) || 0) - (parseFloat(x.getPropertyValue(e, "borderRightWidth")) || 0) - (parseFloat(x.getPropertyValue(e, "paddingLeft")) || 0) - (parseFloat(x.getPropertyValue(e, "paddingRight")) || 0);
                                    return r(), d
                                }
                            }
                            var f;
                            f = o(e) === i ? t.getComputedStyle(e, null) : o(e).computedStyle ? o(e).computedStyle : o(e).computedStyle = t.getComputedStyle(e, null), "borderColor" === n && (n = "borderTopColor"), l = 9 === h && "filter" === n ? f.getPropertyValue(n) : f[n], ("" === l || null === l) && (l = e.style[n]), r()
                        }
                        if ("auto" === l && /^(top|right|bottom|left)$/i.test(n)) {
                            var g = s(e, "position");
                            ("fixed" === g || "absolute" === g && /top|left/i.test(n)) && (l = p(e).position()[n] + "px")
                        }
                        return l
                    }
                    var l;
                    if (x.Hooks.registered[n]) {
                        var u = n,
                            c = x.Hooks.getRoot(u);
                        r === i && (r = x.getPropertyValue(e, x.Names.prefixCheck(c)[0])), x.Normalizations.registered[c] && (r = x.Normalizations.registered[c]("extract", e, r)), l = x.Hooks.extractValue(u, r)
                    } else if (x.Normalizations.registered[n]) {
                        var d, f;
                        d = x.Normalizations.registered[n]("name", e), "transform" !== d && (f = s(e, x.Names.prefixCheck(d)[0]), x.Values.isCSSNullValue(f) && x.Hooks.templates[n] && (f = x.Hooks.templates[n][1])), l = x.Normalizations.registered[n]("extract", e, f)
                    }
                    if (!/^[\d-]/.test(l))
                        if (o(e) && o(e).isSVG && x.Names.SVGAttribute(n))
                            if (/^(height|width)$/i.test(n)) try {
                                l = e.getBBox()[n]
                            } catch (g) {
                                l = 0
                            } else l = e.getAttribute(n);
                        else l = s(e, x.Names.prefixCheck(n)[0]);
                    return x.Values.isCSSNullValue(l) && (l = 0), b.debug >= 2 && console.log("Get " + n + ": " + l), l
                },
                setPropertyValue: function (e, n, i, r, a) {
                    var s = n;
                    if ("scroll" === n) a.container ? a.container["scroll" + a.direction] = i : "Left" === a.direction ? t.scrollTo(i, a.alternateValue) : t.scrollTo(a.alternateValue, i);
                    else if (x.Normalizations.registered[n] && "transform" === x.Normalizations.registered[n]("name", e)) x.Normalizations.registered[n]("inject", e, i), s = "transform", i = o(e).transformCache[n];
                    else {
                        if (x.Hooks.registered[n]) {
                            var l = n,
                                u = x.Hooks.getRoot(n);
                            r = r || x.getPropertyValue(e, u), i = x.Hooks.injectValue(l, i, r), n = u
                        }
                        if (x.Normalizations.registered[n] && (i = x.Normalizations.registered[n]("inject", e, i), n = x.Normalizations.registered[n]("name", e)), s = x.Names.prefixCheck(n)[0], 8 >= h) try {
                            e.style[s] = i
                        } catch (c) {
                            b.debug && console.log("Browser does not support [" + i + "] for [" + s + "]")
                        } else o(e) && o(e).isSVG && x.Names.SVGAttribute(n) ? e.setAttribute(n, i) : e.style[s] = i;
                        b.debug >= 2 && console.log("Set " + n + " (" + s + "): " + i)
                    }
                    return [s, i]
                },
                flushTransformCache: function (e) {
                    function t(t) {
                        return parseFloat(x.getPropertyValue(e, t))
                    }
                    var n = "";
                    if ((h || b.State.isAndroid && !b.State.isChrome) && o(e).isSVG) {
                        var i = {
                            translate: [t("translateX"), t("translateY")],
                            skewX: [t("skewX")],
                            skewY: [t("skewY")],
                            scale: 1 !== t("scale") ? [t("scale"), t("scale")] : [t("scaleX"), t("scaleY")],
                            rotate: [t("rotateZ"), 0, 0]
                        };
                        p.each(o(e).transformCache, function (e) {
                            /^translate/i.test(e) ? e = "translate" : /^scale/i.test(e) ? e = "scale" : /^rotate/i.test(e) && (e = "rotate"), i[e] && (n += e + "(" + i[e].join(" ") + ") ", delete i[e])
                        })
                    } else {
                        var r, a;
                        p.each(o(e).transformCache, function (t) {
                            return r = o(e).transformCache[t], "transformPerspective" === t ? (a = r, !0) : (9 === h && "rotateZ" === t && (t = "rotate"), void (n += t + r + " "))
                        }), a && (n = "perspective" + a + " " + n)
                    }
                    x.setPropertyValue(e, "transform", n)
                }
            };
            x.Hooks.register(), x.Normalizations.register(), b.hook = function (e, t, n) {
                var r = i;
                return e = a(e), p.each(e, function (e, a) {
                    if (o(a) === i && b.init(a), n === i) r === i && (r = b.CSS.getPropertyValue(a, t));
                    else {
                        var s = b.CSS.setPropertyValue(a, t, n);
                        "transform" === s[0] && b.CSS.flushTransformCache(a), r = s
                    }
                }), r
            };
            var k = function () {
                function e() {
                    return s ? O.promise || null : l
                }

                function r() {
                    function e() {
                        function e(e, t) {
                            var n = i,
                                r = i,
                                o = i;
                            return g.isArray(e) ? (n = e[0], !g.isArray(e[1]) && /^[\d-]/.test(e[1]) || g.isFunction(e[1]) || x.RegEx.isHex.test(e[1]) ? o = e[1] : (g.isString(e[1]) && !x.RegEx.isHex.test(e[1]) || g.isArray(e[1])) && (r = t ? e[1] : u(e[1], s.duration), e[2] !== i && (o = e[2]))) : n = e, t || (r = r || s.easing), g.isFunction(n) && (n = n.call(a, S, C)), g.isFunction(o) && (o = o.call(a, S, C)), [n || 0, r, o]
                        }

                        function d(e, t) {
                            var n, i;
                            return i = (t || "0").toString().toLowerCase().replace(/[%A-z]+$/, function (e) {
                                return n = e, ""
                            }), n || (n = x.Values.getUnitType(e)), [i, n]
                        }

                        function h() {
                            var e = {
                                myParent: a.parentNode || n.body,
                                position: x.getPropertyValue(a, "position"),
                                fontSize: x.getPropertyValue(a, "fontSize")
                            },
                                i = e.position === j.lastPosition && e.myParent === j.lastParent,
                                r = e.fontSize === j.lastFontSize;
                            j.lastParent = e.myParent, j.lastPosition = e.position, j.lastFontSize = e.fontSize;
                            var s = 100,
                                l = {};
                            if (r && i) l.emToPx = j.lastEmToPx, l.percentToPxWidth = j.lastPercentToPxWidth, l.percentToPxHeight = j.lastPercentToPxHeight;
                            else {
                                var u = o(a).isSVG ? n.createElementNS("http://www.w3.org/2000/svg", "rect") : n.createElement("div");
                                b.init(u), e.myParent.appendChild(u), p.each(["overflow", "overflowX", "overflowY"], function (e, t) {
                                    b.CSS.setPropertyValue(u, t, "hidden")
                                }), b.CSS.setPropertyValue(u, "position", e.position), b.CSS.setPropertyValue(u, "fontSize", e.fontSize), b.CSS.setPropertyValue(u, "boxSizing", "content-box"), p.each(["minWidth", "maxWidth", "width", "minHeight", "maxHeight", "height"], function (e, t) {
                                    b.CSS.setPropertyValue(u, t, s + "%")
                                }), b.CSS.setPropertyValue(u, "paddingLeft", s + "em"), l.percentToPxWidth = j.lastPercentToPxWidth = (parseFloat(x.getPropertyValue(u, "width", null, !0)) || 1) / s, l.percentToPxHeight = j.lastPercentToPxHeight = (parseFloat(x.getPropertyValue(u, "height", null, !0)) || 1) / s, l.emToPx = j.lastEmToPx = (parseFloat(x.getPropertyValue(u, "paddingLeft")) || 1) / s, e.myParent.removeChild(u)
                            }
                            return null === j.remToPx && (j.remToPx = parseFloat(x.getPropertyValue(n.body, "fontSize")) || 16), null === j.vwToPx && (j.vwToPx = parseFloat(t.innerWidth) / 100, j.vhToPx = parseFloat(t.innerHeight) / 100), l.remToPx = j.remToPx, l.vwToPx = j.vwToPx, l.vhToPx = j.vhToPx, b.debug >= 1 && console.log("Unit ratios: " + JSON.stringify(l), a), l
                        }
                        if (s.begin && 0 === S) try {
                            s.begin.call(f, f)
                        } catch (m) {
                            setTimeout(function () {
                                throw m
                            }, 1)
                        }
                        if ("scroll" === E) {
                            var w, k, T, P = /^x$/i.test(s.axis) ? "Left" : "Top",
                                A = parseFloat(s.offset) || 0;
                            s.container ? g.isWrapped(s.container) || g.isNode(s.container) ? (s.container = s.container[0] || s.container, w = s.container["scroll" + P], T = w + p(a).position()[P.toLowerCase()] + A) : s.container = null : (w = b.State.scrollAnchor[b.State["scrollProperty" + P]], k = b.State.scrollAnchor[b.State["scrollProperty" + ("Left" === P ? "Top" : "Left")]], T = p(a).offset()[P.toLowerCase()] + A), l = {
                                scroll: {
                                    rootPropertyValue: !1,
                                    startValue: w,
                                    currentValue: w,
                                    endValue: T,
                                    unitType: "",
                                    easing: s.easing,
                                    scrollData: {
                                        container: s.container,
                                        direction: P,
                                        alternateValue: k
                                    }
                                },
                                element: a
                            }, b.debug && console.log("tweensContainer (scroll): ", l.scroll, a)
                        } else if ("reverse" === E) {
                            if (!o(a).tweensContainer) return void p.dequeue(a, s.queue);
                            "none" === o(a).opts.display && (o(a).opts.display = "auto"), "hidden" === o(a).opts.visibility && (o(a).opts.visibility = "visible"), o(a).opts.loop = !1, o(a).opts.begin = null, o(a).opts.complete = null, y.easing || delete s.easing, y.duration || delete s.duration, s = p.extend({}, o(a).opts, s);
                            var M = p.extend(!0, {}, o(a).tweensContainer);
                            for (var V in M)
                                if ("element" !== V) {
                                    var _ = M[V].startValue;
                                    M[V].startValue = M[V].currentValue = M[V].endValue, M[V].endValue = _, g.isEmptyObject(y) || (M[V].easing = s.easing), b.debug && console.log("reverse tweensContainer (" + V + "): " + JSON.stringify(M[V]), a)
                                }
                            l = M
                        } else if ("start" === E) {
                            var M;
                            o(a).tweensContainer && o(a).isAnimating === !0 && (M = o(a).tweensContainer), p.each(v, function (t, n) {
                                if (RegExp("^" + x.Lists.colors.join("$|^") + "$").test(t)) {
                                    var r = e(n, !0),
                                        a = r[0],
                                        o = r[1],
                                        s = r[2];
                                    if (x.RegEx.isHex.test(a)) {
                                        for (var l = ["Red", "Green", "Blue"], u = x.Values.hexToRgb(a), c = s ? x.Values.hexToRgb(s) : i, d = 0; d < l.length; d++) {
                                            var p = [u[d]];
                                            o && p.push(o), c !== i && p.push(c[d]), v[t + l[d]] = p
                                        }
                                        delete v[t]
                                    }
                                }
                            });
                            for (var I in v) {
                                var $ = e(v[I]),
                                    F = $[0],
                                    q = $[1],
                                    N = $[2];
                                I = x.Names.camelCase(I);
                                var R = x.Hooks.getRoot(I),
                                    z = !1;
                                if (o(a).isSVG || "tween" === R || x.Names.prefixCheck(R)[1] !== !1 || x.Normalizations.registered[R] !== i) {
                                    (s.display !== i && null !== s.display && "none" !== s.display || s.visibility !== i && "hidden" !== s.visibility) && /opacity|filter/.test(I) && !N && 0 !== F && (N = 0), s._cacheValues && M && M[I] ? (N === i && (N = M[I].endValue + M[I].unitType), z = o(a).rootPropertyValueCache[R]) : x.Hooks.registered[I] ? N === i ? (z = x.getPropertyValue(a, R), N = x.getPropertyValue(a, I, z)) : z = x.Hooks.templates[R][1] : N === i && (N = x.getPropertyValue(a, I));
                                    var W, Q, L, H = !1;
                                    if (W = d(I, N), N = W[0], L = W[1], W = d(I, F), F = W[0].replace(/^([+-\/*])=/, function (e, t) {
                                            return H = t, ""
                                    }), Q = W[1], N = parseFloat(N) || 0, F = parseFloat(F) || 0, "%" === Q && (/^(fontSize|lineHeight)$/.test(I) ? (F /= 100, Q = "em") : /^scale/.test(I) ? (F /= 100, Q = "") : /(Red|Green|Blue)$/i.test(I) && (F = F / 100 * 255, Q = "")), /[\/*]/.test(H)) Q = L;
                                    else if (L !== Q && 0 !== N)
                                        if (0 === F) Q = L;
                                        else {
                                            r = r || h();
                                            var Y = /margin|padding|left|right|width|text|word|letter/i.test(I) || /X$/.test(I) || "x" === I ? "x" : "y";
                                            switch (L) {
                                                case "%":
                                                    N *= "x" === Y ? r.percentToPxWidth : r.percentToPxHeight;
                                                    break;
                                                case "px":
                                                    break;
                                                default:
                                                    N *= r[L + "ToPx"]
                                            }
                                            switch (Q) {
                                                case "%":
                                                    N *= 1 / ("x" === Y ? r.percentToPxWidth : r.percentToPxHeight);
                                                    break;
                                                case "px":
                                                    break;
                                                default:
                                                    N *= 1 / r[Q + "ToPx"]
                                            }
                                        }
                                    switch (H) {
                                        case "+":
                                            F = N + F;
                                            break;
                                        case "-":
                                            F = N - F;
                                            break;
                                        case "*":
                                            F = N * F;
                                            break;
                                        case "/":
                                            F = N / F
                                    }
                                    l[I] = {
                                        rootPropertyValue: z,
                                        startValue: N,
                                        currentValue: N,
                                        endValue: F,
                                        unitType: Q,
                                        easing: q
                                    }, b.debug && console.log("tweensContainer (" + I + "): " + JSON.stringify(l[I]), a)
                                } else b.debug && console.log("Skipping [" + R + "] due to a lack of browser support.")
                            }
                            l.element = a
                        }
                        l.element && (x.Values.addClass(a, "velocity-animating"), D.push(l), "" === s.queue && (o(a).tweensContainer = l, o(a).opts = s), o(a).isAnimating = !0, S === C - 1 ? (b.State.calls.push([D, f, s, null, O.resolver]), b.State.isTicking === !1 && (b.State.isTicking = !0, c())) : S++)
                    }
                    var r, a = this,
                        s = p.extend({}, b.defaults, y),
                        l = {};
                    switch (o(a) === i && b.init(a), parseFloat(s.delay) && s.queue !== !1 && p.queue(a, s.queue, function (e) {
                        b.velocityQueueEntryFlag = !0, o(a).delayTimer = {
                        setTimeout: setTimeout(e, parseFloat(s.delay)),
                        next: e
                    }
                    }), s.duration.toString().toLowerCase()) {
                        case "fast":
                            s.duration = 10;
                            break;
                        case "normal":
                            s.duration = m;
                            break;
                        case "slow":
                            s.duration = 50;
                            break;
                        default:
                            s.duration = parseFloat(s.duration) || 1
                    }
                    b.mock !== !1 && (b.mock === !0 ? s.duration = s.delay = 1 : (s.duration *= parseFloat(b.mock) || 1, s.delay *= parseFloat(b.mock) || 1)), s.easing = u(s.easing, s.duration), s.begin && !g.isFunction(s.begin) && (s.begin = null), s.progress && !g.isFunction(s.progress) && (s.progress = null), s.complete && !g.isFunction(s.complete) && (s.complete = null), s.display !== i && null !== s.display && (s.display = s.display.toString().toLowerCase(), "auto" === s.display && (s.display = b.CSS.Values.getDisplayType(a))), s.visibility !== i && null !== s.visibility && (s.visibility = s.visibility.toString().toLowerCase()), s.mobileHA = s.mobileHA && b.State.isMobile && !b.State.isGingerbread, s.queue === !1 ? s.delay ? setTimeout(e, s.delay) : e() : p.queue(a, s.queue, function (t, n) {
                        return n === !0 ? (O.promise && O.resolver(f), !0) : (b.velocityQueueEntryFlag = !0, void e(t))
                    }), "" !== s.queue && "fx" !== s.queue || "inprogress" === p.queue(a)[0] || p.dequeue(a)
                }
                var s, l, h, f, v, y, w = arguments[0] && (arguments[0].p || p.isPlainObject(arguments[0].properties) && !arguments[0].properties.names || g.isString(arguments[0].properties));
                if (g.isWrapped(this) ? (s = !1, h = 0, f = this, l = this) : (s = !0, h = 1, f = w ? arguments[0].elements || arguments[0].e : arguments[0]), f = a(f)) {
                    w ? (v = arguments[0].properties || arguments[0].p, y = arguments[0].options || arguments[0].o) : (v = arguments[h], y = arguments[h + 1]);
                    var C = f.length,
                        S = 0;
                    if (!/^(stop|finish)$/i.test(v) && !p.isPlainObject(y)) {
                        var T = h + 1;
                        y = {};
                        for (var P = T; P < arguments.length; P++) g.isArray(arguments[P]) || !/^(fast|normal|slow)$/i.test(arguments[P]) && !/^\d/.test(arguments[P]) ? g.isString(arguments[P]) || g.isArray(arguments[P]) ? y.easing = arguments[P] : g.isFunction(arguments[P]) && (y.complete = arguments[P]) : y.duration = arguments[P]
                    }
                    var O = {
                        promise: null,
                        resolver: null,
                        rejecter: null
                    };
                    s && b.Promise && (O.promise = new b.Promise(function (e, t) {
                        O.resolver = e, O.rejecter = t
                    }));
                    var E;
                    switch (v) {
                        case "scroll":
                            E = "scroll";
                            break;
                        case "reverse":
                            E = "reverse";
                            break;
                        case "finish":
                        case "stop":
                            p.each(f, function (e, t) {
                                o(t) && o(t).delayTimer && (clearTimeout(o(t).delayTimer.setTimeout), o(t).delayTimer.next && o(t).delayTimer.next(), delete o(t).delayTimer)
                            });
                            var A = [];
                            return p.each(b.State.calls, function (e, t) {
                                t && p.each(t[1], function (n, r) {
                                    var a = y === i ? "" : y;
                                    return a === !0 || t[2].queue === a || y === i && t[2].queue === !1 ? void p.each(f, function (n, i) {
                                        i === r && ((y === !0 || g.isString(y)) && (p.each(p.queue(i, g.isString(y) ? y : ""), function (e, t) {
                                            g.isFunction(t) && t(null, !0)
                                        }), p.queue(i, g.isString(y) ? y : "", [])), "stop" === v ? (o(i) && o(i).tweensContainer && a !== !1 && p.each(o(i).tweensContainer, function (e, t) {
                                            t.endValue = t.currentValue
                                        }), A.push(e)) : "finish" === v && (t[2].duration = 1))
                                    }) : !0
                                })
                            }), "stop" === v && (p.each(A, function (e, t) {
                                d(t, !0)
                            }), O.promise && O.resolver(f)), e();
                        default:
                            if (!p.isPlainObject(v) || g.isEmptyObject(v)) {
                                if (g.isString(v) && b.Redirects[v]) {
                                    var M = p.extend({}, y),
                                        V = M.duration,
                                        _ = M.delay || 0;
                                    return M.backwards === !0 && (f = p.extend(!0, [], f).reverse()), p.each(f, function (e, t) {
                                        parseFloat(M.stagger) ? M.delay = _ + parseFloat(M.stagger) * e : g.isFunction(M.stagger) && (M.delay = _ + M.stagger.call(t, e, C)), M.drag && (M.duration = parseFloat(V) || (/^(callout|transition)/.test(v) ? 1e3 : m), M.duration = Math.max(M.duration * (M.backwards ? 1 - e / C : (e + 1) / C), .75 * M.duration, 200)), b.Redirects[v].call(t, t, M || {}, e, C, f, O.promise ? O : i)
                                    }), e()
                                }
                                var I = "Velocity: First argument (" + v + ") was not a property map, a known action, or a registered redirect. Aborting.";
                                return O.promise ? O.rejecter(new Error(I)) : console.log(I), e()
                            }
                            E = "start"
                    }
                    var j = {
                        lastParent: null,
                        lastPosition: null,
                        lastFontSize: null,
                        lastPercentToPxWidth: null,
                        lastPercentToPxHeight: null,
                        lastEmToPx: null,
                        remToPx: null,
                        vwToPx: null,
                        vhToPx: null
                    },
                        D = [];
                    p.each(f, function (e, t) {
                        g.isNode(t) && r.call(t)
                    });
                    var $, M = p.extend({}, b.defaults, y);
                    if (M.loop = parseInt(M.loop), $ = 2 * M.loop - 1, M.loop)
                        for (var F = 0; $ > F; F++) {
                            var q = {
                                delay: M.delay,
                                progress: M.progress
                            };
                            F === $ - 1 && (q.display = M.display, q.visibility = M.visibility, q.complete = M.complete), k(f, "reverse", q)
                        }
                    return e()
                }
            };
            b = p.extend(k, b), b.animate = k;
            var C = t.requestAnimationFrame || f;
            return b.State.isMobile || n.hidden === i || n.addEventListener("visibilitychange", function () {
                n.hidden ? (C = function (e) {
                    return setTimeout(function () {
                        e(!0)
                    }, 16)
                }, c()) : C = t.requestAnimationFrame || f
            }), e.Velocity = b, e !== t && (e.fn.velocity = k, e.fn.velocity.defaults = b.defaults), p.each(["Down", "Up"], function (e, t) {
                b.Redirects["slide" + t] = function (e, n, r, a, o, s) {
                    var l = p.extend({}, n),
                        u = l.begin,
                        c = l.complete,
                        d = {
                            height: "",
                            marginTop: "",
                            marginBottom: "",
                            paddingTop: "",
                            paddingBottom: ""
                        },
                        h = {};
                    l.display === i && (l.display = "Down" === t ? "inline" === b.CSS.Values.getDisplayType(e) ? "inline-block" : "block" : "none"), l.begin = function () {
                        u && u.call(o, o);
                        for (var n in d) {
                            h[n] = e.style[n];
                            var i = b.CSS.getPropertyValue(e, n);
                            d[n] = "Down" === t ? [i, 0] : [0, i]
                        }
                        h.overflow = e.style.overflow, e.style.overflow = "hidden"
                    }, l.complete = function () {
                        for (var t in h) e.style[t] = h[t];
                        c && c.call(o, o), s && s.resolver(o)
                    }, b(e, d, l)
                }
            }), p.each(["In", "Out"], function (e, t) {
                b.Redirects["fade" + t] = function (e, n, r, a, o, s) {
                    var l = p.extend({}, n),
                        u = {
                            opacity: "In" === t ? 1 : 0
                        },
                        c = l.complete;
                    l.complete = r !== a - 1 ? l.begin = null : function () {
                        c && c.call(o, o), s && s.resolver(o)
                    }, l.display === i && (l.display = "In" === t ? "auto" : "none"), b(this, u, l)
                }
            }), b
        }(window.jQuery || window.Zepto || window, window, document)
    })
var Vel;
Vel = $ ? $.Velocity : Velocity,
    function (e) {
        e.fn.extend({
            openModal: function (t) {
                var n = this,
                    i = e('<div id="lean-overlay"></div>');
                e("body").append(i);
                var r = {
                    opacity: .5,
                    in_duration: 50,
                    out_duration: 50,
                    ready: void 0,
                    complete: void 0,
                    dismissible: !0
                };
                t = e.extend(r, t), t.dismissible && (e("#lean-overlay").click(function () {
                    e(n).closeModal(t)
                }),
                e(document).on("keyup.leanModal", function (i) {
                    27 === i.keyCode && e(n).closeModal(t)
                })),
                e(n).find(".modal-close").click(function () {
                    e(n).closeModal(t)
                }),
                e("#lean-overlay").css({
                    display: "block",
                    opacity: 0
                }),
                e(n).css({
                    display: "block",
                    opacity: 0
                }),
                e("#lean-overlay").velocity({
                    opacity: t.opacity
                },
                {
                    duration: t.in_duration,
                    queue: !1,
                    ease: "easeOutCubic"
                }),
                e(n).hasClass("bottom-sheet") ? e(n).velocity({
                    bottom: "0",
                    opacity: 1
                }, {
                    duration: t.in_duration,
                    queue: !1,
                    ease: "easeOutCubic",
                    complete: function () {
                        "function" == typeof t.ready && t.ready()
                    }
                }) : (e(n).css({
                    top: "4%"
                }), e(n).velocity({
                    top: "10%",
                    opacity: 1
                }, {
                    duration: t.in_duration,
                    queue: !1,
                    ease: "easeOutCubic",
                    complete: function () {
                        "function" == typeof t.ready && t.ready()
                    }
                }))
            }
        }), e.fn.extend({
            closeModal: function (t) {
                var n = {
                    out_duration: 50,
                    complete: void 0
                },
                    t = e.extend(n, t);
                e(".modal-close").off(), e(document).off("keyup.leanModal"), e("#lean-overlay").velocity({
                    opacity: 0
                }, {
                    duration: t.out_duration,
                    queue: !1,
                    ease: "easeOutQuart"
                }), e(this).hasClass("bottom-sheet") ? e(this).velocity({
                    bottom: "-100%",
                    opacity: 0
                }, {
                    duration: t.out_duration,
                    queue: !1,
                    ease: "easeOutCubic",
                    complete: function () {
                        e("#lean-overlay").css({
                            display: "none"
                        }), "function" == typeof t.complete && t.complete(), e("#lean-overlay").remove()
                    }
                }) : e(this).fadeOut(t.out_duration, function () {
                    e(this).css({
                        top: 0
                    }), e("#lean-overlay").css({
                        display: "none"
                    }), "function" == typeof t.complete && t.complete(), e("#lean-overlay").remove()
                })
            }
        }), e.fn.extend({
            leanModal: function (t) {
                return this.each(function () {
                    e(this).click(function (n) {
                        var i = e(this).attr("href") || "#" + e(this).data("target");
                        e(i).openModal(t), n.preventDefault()
                    })
                })
            }
        })
    }(jQuery),
function createCookie(name, value, days) {
    if (days) {
        var date = new Date(); date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    } else expires = ""; document.cookie = name + "=" + value + expires + "; path=/";
}
function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i]; while (c.charAt(0) == ' ') c = c.substring(1, c.length); if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    } return null;
}
//g_c && s_cs
function s_c(e, t, n) {
    var r = new Date; r.setTime(r.getTime() + 24 * n * 60 * 60 * 1e3); var i = "expires=" + r.toUTCString(); document.cookie = e + "=" + t + "; " + i
} function g_c(e) { for (var t = e + "=", n = document.cookie.split(";"), r = 0; r < n.length; r++) { for (var i = n[r]; " " == i.charAt(0) ;) i = i.substring(1); if (0 == i.indexOf(t)) return i.substring(t.length, i.length) } return "" }
///*! lazysizes - v1.1.2 -  Licensed MIT */
//!function (a, b) {
//    var c = b(a, a.document); a.lazySizes = c, "object" == typeof module && module.exports ? module.exports = c : "function" == typeof define && define.amd && define(c)
//}(window, function (a, b) { "use strict"; if (b.getElementsByClassName) { var c, d = b.documentElement, e = a.addEventListener, f = a.setTimeout, g = a.requestAnimationFrame || f, h = /^picture$/i, i = ["load", "error", "lazyincluded", "_lazyloaded"], j = function (a, b) { var c = new RegExp("(\\s|^)" + b + "(\\s|$)"); return a.className.match(c) && c }, k = function (a, b) { j(a, b) || (a.className += " " + b) }, l = function (a, b) { var c; (c = j(a, b)) && (a.className = a.className.replace(c, " ")) }, m = function (a, b, c) { var d = c ? "addEventListener" : "removeEventListener"; c && m(a, b), i.forEach(function (c) { a[d](c, b) }) }, n = function (a, c, d, e, f) { var g = b.createEvent("CustomEvent"); return g.initCustomEvent(c, !e, !f, d), g.details = g.detail, a.dispatchEvent(g), g }, o = function (b, d) { var e; a.HTMLPictureElement || ((e = a.picturefill || a.respimage || c.pf) ? e({ reevaluate: !0, reparse: !0, elements: [b] }) : d && d.src && (b.src = d.src)) }, p = function (a, b) { return getComputedStyle(a, null)[b] }, q = function (a, b, d) { for (d = d || a.offsetWidth; d < c.minSize && b && !a._lazysizesWidth;) d = b.offsetWidth, b = b.parentNode; return d }, r = function (b) { var d, e = 0, h = a.Date, i = function () { d = !1, e = h.now(), b() }, j = function () { f(i) }, k = function () { g(j) }; return function () { if (!d) { var a = c.throttle - (h.now() - e); d = !0, 9 > a && (a = 9), f(k, a) } } }, s = function () { var i, q, s, u, v, w, x, y, z, A, B, C, D, E = /^img$/i, F = /^iframe$/i, G = "onscroll" in a && !/glebot/.test(navigator.userAgent), H = 0, I = 0, J = 0, K = 1, L = function (a) { J--, a && a.target && m(a.target, L), (!a || 0 > J || !a.target) && (J = 0) }, M = function (a, b) { var c, d = a, e = "hidden" != p(a, "visibility"); for (y -= b, B += b, z -= b, A += b; e && (d = d.offsetParent) ;) e = (p(d, "opacity") || 1) > 0, e && "visible" != p(d, "overflow") && (c = d.getBoundingClientRect(), e = A > c.left && z < c.right && B > c.top - 1 && y < c.bottom + 1); return e }, N = function () { var a, b, d, e, f, g, h, j, k; if ((v = c.loadMode) && 8 > J && (a = i.length)) { for (b = 0, K++, D > I && 1 > J && K > 3 && v > 2 ? (I = D, K = 0) : I = I != C && v > 1 && K > 2 && 6 > J ? C : H; a > b; b++) i[b] && !i[b]._lazyRace && (G ? ((j = i[b].getAttribute("data-expand")) && (g = 1 * j) || (g = I), k !== g && (w = innerWidth + g, x = innerHeight + g, h = -1 * g, k = g), d = i[b].getBoundingClientRect(), (B = d.bottom) >= h && (y = d.top) <= x && (A = d.right) >= h && (z = d.left) <= w && (B || A || z || y) && (s && 3 > J && 4 > K && !j && v > 2 || M(i[b], g)) ? (S(i[b], !1, d.width), f = !0) : !f && s && !e && 3 > J && 4 > K && v > 2 && (q[0] || c.preloadAfterLoad) && (q[0] || !j && (B || A || z || y || "auto" != i[b].getAttribute(c.sizesAttr))) && (e = q[0] || i[b])) : S(i[b])); e && !f && S(e) } }, O = r(N), P = function (a) { k(a.target, c.loadedClass), l(a.target, c.loadingClass), m(a.target, P) }, Q = function (a, b) { try { a.contentWindow.location.replace(b) } catch (c) { a.setAttribute("src", b) } }, R = function () { var a, b = [], c = function () { for (; b.length;) b.shift()(); a = !1 }; return function (d) { b.push(d), a || (a = !0, g(c)) } }(), S = function (a, b, d) { var e, g, i, p, q, r, v, w, x, y, z, A = a.currentSrc || a.src, B = E.test(a.nodeName), C = a.getAttribute(c.sizesAttr) || a.getAttribute("sizes"), D = "auto" == C; (!D && s || !B || !A || a.complete || j(a, c.errorClass)) && (a._lazyRace = !0, J++, R(function () { if (a._lazyRace && delete a._lazyRace, l(a, c.lazyClass), !(x = n(a, "lazybeforeunveil", { force: !!b })).defaultPrevented) { if (C && (D ? (t.updateElem(a, !0, d), k(a, c.autosizesClass)) : a.setAttribute("sizes", C)), r = a.getAttribute(c.srcsetAttr), q = a.getAttribute(c.srcAttr), B && (v = a.parentNode, w = v && h.test(v.nodeName || "")), y = x.detail.firesLoad || "src" in a && (r || q || w), x = { target: a }, y && (m(a, L, !0), clearTimeout(u), u = f(L, 2500), k(a, c.loadingClass), m(a, P, !0)), w) for (e = v.getElementsByTagName("source"), g = 0, i = e.length; i > g; g++) (z = c.customMedia[e[g].getAttribute("data-media") || e[g].getAttribute("media")]) && e[g].setAttribute("media", z), p = e[g].getAttribute(c.srcsetAttr), p && e[g].setAttribute("srcset", p); r ? a.setAttribute("srcset", r) : q && (F.test(a.nodeName) ? Q(a, q) : a.setAttribute("src", q)), (r || w) && o(a, { src: q }) } (!y || a.complete && A == (a.currentSrc || a.src)) && (y ? L(x) : J--, P(x)) })) }, T = function () { var a, b = function () { c.loadMode = 3, O() }; s = !0, K += 8, c.loadMode = 3, e("scroll", function () { 3 == c.loadMode && (c.loadMode = 2), clearTimeout(a), a = f(b, 99) }, !0) }; return { _: function () { i = b.getElementsByClassName(c.lazyClass), q = b.getElementsByClassName(c.lazyClass + " " + c.preloadClass), C = c.expand, D = Math.round(C * c.expFactor), e("scroll", O, !0), e("resize", O, !0), a.MutationObserver ? new MutationObserver(O).observe(d, { childList: !0, subtree: !0, attributes: !0 }) : (d.addEventListener("DOMNodeInserted", O, !0), d.addEventListener("DOMAttrModified", O, !0), setInterval(O, 999)), e("hashchange", O, !0), ["focus", "mouseover", "click", "load", "transitionend", "animationend", "webkitAnimationEnd"].forEach(function (a) { b.addEventListener(a, O, !0) }), (s = /d$|^c/.test(b.readyState)) ? T() : (e("load", T), b.addEventListener("DOMContentLoaded", O)), O() }, checkElems: O, unveil: S } }(), t = function () { var a, d = function (a, b, c) { var d, e, f, g, i = a.parentNode; if (i && (c = q(a, i, c), g = n(a, "lazybeforesizes", { width: c, dataAttr: !!b }), !g.defaultPrevented && (c = g.detail.width, c && c !== a._lazysizesWidth))) { if (a._lazysizesWidth = c, c += "px", a.setAttribute("sizes", c), h.test(i.nodeName || "")) for (d = i.getElementsByTagName("source"), e = 0, f = d.length; f > e; e++) d[e].setAttribute("sizes", c); g.detail.dataAttr || o(a, g.detail) } }, f = function () { var b, c = a.length; if (c) for (b = 0; c > b; b++) d(a[b]) }, g = r(f); return { _: function () { a = b.getElementsByClassName(c.autosizesClass), e("resize", g) }, checkElems: g, updateElem: d } }(), u = function () { u.i || (u.i = !0, t._(), s._()) }; return function () { var b, d = { lazyClass: "lazyload", loadedClass: "lazyloaded", loadingClass: "lazyloading", preloadClass: "lazypreload", errorClass: "lazyerror", autosizesClass: "lazyautosizes", srcAttr: "data-src", srcsetAttr: "data-srcset", sizesAttr: "data-sizes", minSize: 40, customMedia: {}, init: !0, expFactor: 2, expand: 359, loadMode: 2, throttle: 125 }; c = a.lazySizesConfig || a.lazysizesConfig || {}; for (b in d) b in c || (c[b] = d[b]); a.lazySizesConfig = c, f(function () { c.init && u() }) }(), { cfg: c, autoSizer: t, loader: s, init: u, uP: o, aC: k, rC: l, hC: j, fire: n, gW: q } } });
/*! carousel -  Licensed MIT */
"function" !== typeof Object.create && (Object.create = function (f) { function g() { } g.prototype = f; return new g });
(function (f, g, k) {
    var l = {
        init: function (a, b) { this.$elem = f(b); this.options = f.extend({}, f.fn.owlCarousel.options, this.$elem.data(), a); this.userOptions = a; this.loadContent() }, loadContent: function () {
            function a(a) { var d, e = ""; if ("function" === typeof b.options.jsonSuccess) b.options.jsonSuccess.apply(this, [a]); else { for (d in a.owl) a.owl.hasOwnProperty(d) && (e += a.owl[d].item); b.$elem.html(e) } b.logIn() } var b = this, e; "function" === typeof b.options.beforeInit && b.options.beforeInit.apply(this, [b.$elem]); "string" === typeof b.options.jsonPath ?
            (e = b.options.jsonPath, f.getJSON(e, a)) : b.logIn()
        }, logIn: function () { this.$elem.data("owl-originalStyles", this.$elem.attr("style")); this.$elem.data("owl-originalClasses", this.$elem.attr("class")); this.$elem.css({ opacity: 0 }); this.orignalItems = this.options.items; this.checkBrowser(); this.wrapperWidth = 0; this.checkVisible = null; this.setVars() }, setVars: function () {
            if (0 === this.$elem.children().length) return !1; this.baseClass(); this.eventTypes(); this.$userItems = this.$elem.children(); this.itemsAmount = this.$userItems.length;
            this.wrapItems(); this.$owlItems = this.$elem.find(".owl-item"); this.$owlWrapper = this.$elem.find(".owl-wrapper"); this.playDirection = "next"; this.prevItem = 0; this.prevArr = [0]; this.currentItem = 0; this.customEvents(); this.onStartup()
        }, onStartup: function () {
            this.updateItems(); this.calculateAll(); this.buildControls(); this.updateControls(); this.response(); this.moveEvents(); this.stopOnHover(); this.owlStatus(); !1 !== this.options.transitionStyle && this.transitionTypes(this.options.transitionStyle); !0 === this.options.autoPlay &&
            (this.options.autoPlay = 5E3); this.play(); this.$elem.find(".owl-wrapper").css("display", "block"); this.$elem.is(":visible") ? this.$elem.css("opacity", 1) : this.watchVisibility(); this.onstartup = !1; this.eachMoveUpdate(); "function" === typeof this.options.afterInit && this.options.afterInit.apply(this, [this.$elem])
        }, eachMoveUpdate: function () {
            !0 === this.options.lazyLoad && this.lazyLoad(); !0 === this.options.autoHeight && this.autoHeight(); this.onVisibleItems(); "function" === typeof this.options.afterAction && this.options.afterAction.apply(this,
            [this.$elem])
        }, updateVars: function () { "function" === typeof this.options.beforeUpdate && this.options.beforeUpdate.apply(this, [this.$elem]); this.watchVisibility(); this.updateItems(); this.calculateAll(); this.updatePosition(); this.updateControls(); this.eachMoveUpdate(); "function" === typeof this.options.afterUpdate && this.options.afterUpdate.apply(this, [this.$elem]) }, reload: function () { var a = this; g.setTimeout(function () { a.updateVars() }, 0) }, watchVisibility: function () {
            var a = this; if (!1 === a.$elem.is(":visible")) a.$elem.css({ opacity: 0 }),
            g.clearInterval(a.autoPlayInterval), g.clearInterval(a.checkVisible); else return !1; a.checkVisible = g.setInterval(function () { a.$elem.is(":visible") && (a.reload(), a.$elem.animate({ opacity: 1 }, 200), g.clearInterval(a.checkVisible)) }, 500)
        }, wrapItems: function () { this.$userItems.wrapAll('<div class="owl-wrapper">').wrap('<div class="owl-item"></div>'); this.$elem.find(".owl-wrapper").wrap('<div class="owl-wrapper-outer">'); this.wrapperOuter = this.$elem.find(".owl-wrapper-outer"); this.$elem.css("display", "block") },
        baseClass: function () { var a = this.$elem.hasClass(this.options.baseClass), b = this.$elem.hasClass(this.options.theme); a || this.$elem.addClass(this.options.baseClass); b || this.$elem.addClass(this.options.theme) }, updateItems: function () {
            var a, b; if (!1 === this.options.responsive) return !1; if (!0 === this.options.singleItem) return this.options.items = this.orignalItems = 1, this.options.itemsCustom = !1, this.options.itemsDesktop = !1, this.options.itemsDesktopSmall = !1, this.options.itemsTablet = !1, this.options.itemsTabletSmall =
            !1, this.options.itemsMobile = !1; a = f(this.options.responsiveBaseWidth).width(); a > (this.options.itemsDesktop[0] || this.orignalItems) && (this.options.items = this.orignalItems); if (!1 !== this.options.itemsCustom) for (this.options.itemsCustom.sort(function (a, b) { return a[0] - b[0] }), b = 0; b < this.options.itemsCustom.length; b += 1) this.options.itemsCustom[b][0] <= a && (this.options.items = this.options.itemsCustom[b][1]); else a <= this.options.itemsDesktop[0] && !1 !== this.options.itemsDesktop && (this.options.items = this.options.itemsDesktop[1]),
            a <= this.options.itemsDesktopSmall[0] && !1 !== this.options.itemsDesktopSmall && (this.options.items = this.options.itemsDesktopSmall[1]), a <= this.options.itemsTablet[0] && !1 !== this.options.itemsTablet && (this.options.items = this.options.itemsTablet[1]), a <= this.options.itemsTabletSmall[0] && !1 !== this.options.itemsTabletSmall && (this.options.items = this.options.itemsTabletSmall[1]), a <= this.options.itemsMobile[0] && !1 !== this.options.itemsMobile && (this.options.items = this.options.itemsMobile[1]); this.options.items > this.itemsAmount &&
            !0 === this.options.itemsScaleUp && (this.options.items = this.itemsAmount)
        }, response: function () { var a = this, b, e; if (!0 !== a.options.responsive) return !1; e = f(g).width(); a.resizer = function () { f(g).width() !== e && (!1 !== a.options.autoPlay && g.clearInterval(a.autoPlayInterval), g.clearTimeout(b), b = g.setTimeout(function () { e = f(g).width(); a.updateVars() }, a.options.responsiveRefreshRate)) }; f(g).resize(a.resizer) }, updatePosition: function () { this.jumpTo(this.currentItem); !1 !== this.options.autoPlay && this.checkAp() }, appendItemsSizes: function () {
            var a =
            this, b = 0, e = a.itemsAmount - a.options.items; a.$owlItems.each(function (c) { var d = f(this); d.css({ width: a.itemWidth }).data("owl-item", Number(c)); if (0 === c % a.options.items || c === e) c > e || (b += 1); d.data("owl-roundPages", b) })
        }, appendWrapperSizes: function () { this.$owlWrapper.css({ width: this.$owlItems.length * this.itemWidth * 2, left: 0 }); this.appendItemsSizes() }, calculateAll: function () { this.calculateWidth(); this.appendWrapperSizes(); this.loops(); this.max() }, calculateWidth: function () {
            this.itemWidth = Math.round(this.$elem.width() /
            this.options.items)
        }, max: function () { var a = -1 * (this.itemsAmount * this.itemWidth - this.options.items * this.itemWidth); this.options.items > this.itemsAmount ? this.maximumPixels = a = this.maximumItem = 0 : (this.maximumItem = this.itemsAmount - this.options.items, this.maximumPixels = a); return a }, min: function () { return 0 }, loops: function () {
            var a = 0, b = 0, e, c; this.positionsInArray = [0]; this.pagesInArray = []; for (e = 0; e < this.itemsAmount; e += 1) b += this.itemWidth, this.positionsInArray.push(-b), !0 === this.options.scrollPerPage && (c = f(this.$owlItems[e]),
            c = c.data("owl-roundPages"), c !== a && (this.pagesInArray[a] = this.positionsInArray[e], a = c))
        }, buildControls: function () { if (!0 === this.options.navigation || !0 === this.options.pagination) this.owlControls = f('<div class="owl-controls"/>').toggleClass("clickable", !this.browser.isTouch).appendTo(this.$elem); !0 === this.options.pagination && this.buildPagination(); !0 === this.options.navigation && this.buildButtons() }, buildButtons: function () {
            var a = this, b = f('<div class="owl-buttons"/>'); a.owlControls.append(b); a.buttonPrev =
            f("<div/>", { "class": "owl-prev", html: a.options.navigationText[0] || "" }); a.buttonNext = f("<div/>", { "class": "owl-next", html: a.options.navigationText[1] || "" }); b.append(a.buttonPrev).append(a.buttonNext); b.on("touchstart.owlControls mousedown.owlControls", 'div[class^="owl"]', function (a) { a.preventDefault() }); b.on("touchend.owlControls mouseup.owlControls", 'div[class^="owl"]', function (b) { b.preventDefault(); f(this).hasClass("owl-next") ? a.next() : a.prev() })
        }, buildPagination: function () {
            var a = this; a.paginationWrapper =
            f('<div class="owl-pagination"/>'); a.owlControls.append(a.paginationWrapper); a.paginationWrapper.on("touchend.owlControls mouseup.owlControls", ".owl-page", function (b) { b.preventDefault(); Number(f(this).data("owl-page")) !== a.currentItem && a.goTo(Number(f(this).data("owl-page")), !0) })
        }, updatePagination: function () {
            var a, b, e, c, d, g; if (!1 === this.options.pagination) return !1; this.paginationWrapper.html(""); a = 0; b = this.itemsAmount - this.itemsAmount % this.options.items; for (c = 0; c < this.itemsAmount; c += 1) 0 === c % this.options.items &&
            (a += 1, b === c && (e = this.itemsAmount - this.options.items), d = f("<div/>", { "class": "owl-page" }), g = f("<span></span>", { text: !0 === this.options.paginationNumbers ? a : "", "class": !0 === this.options.paginationNumbers ? "owl-numbers" : "" }), d.append(g), d.data("owl-page", b === c ? e : c), d.data("owl-roundPages", a), this.paginationWrapper.append(d)); this.checkPagination()
        }, checkPagination: function () {
            var a = this; if (!1 === a.options.pagination) return !1; a.paginationWrapper.find(".owl-page").each(function () {
                f(this).data("owl-roundPages") ===
                f(a.$owlItems[a.currentItem]).data("owl-roundPages") && (a.paginationWrapper.find(".owl-page").removeClass("active"), f(this).addClass("active"))
            })
        }, checkNavigation: function () {
            if (!1 === this.options.navigation) return !1; !1 === this.options.rewindNav && (0 === this.currentItem && 0 === this.maximumItem ? (this.buttonPrev.addClass("disabled"), this.buttonNext.addClass("disabled")) : 0 === this.currentItem && 0 !== this.maximumItem ? (this.buttonPrev.addClass("disabled"), this.buttonNext.removeClass("disabled")) : this.currentItem ===
            this.maximumItem ? (this.buttonPrev.removeClass("disabled"), this.buttonNext.addClass("disabled")) : 0 !== this.currentItem && this.currentItem !== this.maximumItem && (this.buttonPrev.removeClass("disabled"), this.buttonNext.removeClass("disabled")))
        }, updateControls: function () { this.updatePagination(); this.checkNavigation(); this.owlControls && (this.options.items >= this.itemsAmount ? this.owlControls.hide() : this.owlControls.show()) }, destroyControls: function () { this.owlControls && this.owlControls.remove() }, next: function (a) {
            if (this.isTransition) return !1;
            this.currentItem += !0 === this.options.scrollPerPage ? this.options.items : 1; if (this.currentItem > this.maximumItem + (!0 === this.options.scrollPerPage ? this.options.items - 1 : 0)) if (!0 === this.options.rewindNav) this.currentItem = 0, a = "rewind"; else return this.currentItem = this.maximumItem, !1; this.goTo(this.currentItem, a)
        }, prev: function (a) {
            if (this.isTransition) return !1; this.currentItem = !0 === this.options.scrollPerPage && 0 < this.currentItem && this.currentItem < this.options.items ? 0 : this.currentItem - (!0 === this.options.scrollPerPage ?
            this.options.items : 1); if (0 > this.currentItem) if (!0 === this.options.rewindNav) this.currentItem = this.maximumItem, a = "rewind"; else return this.currentItem = 0, !1; this.goTo(this.currentItem, a)
        }, goTo: function (a, b, e) {
            var c = this; if (c.isTransition) return !1; "function" === typeof c.options.beforeMove && c.options.beforeMove.apply(this, [c.$elem]); a >= c.maximumItem ? a = c.maximumItem : 0 >= a && (a = 0); c.currentItem = c.owl.currentItem = a; if (!1 !== c.options.transitionStyle && "drag" !== e && 1 === c.options.items && !0 === c.browser.support3d) return c.swapSpeed(0),
            !0 === c.browser.support3d ? c.transition3d(c.positionsInArray[a]) : c.css2slide(c.positionsInArray[a], 1), c.afterGo(), c.singleItemTransition(), !1; a = c.positionsInArray[a]; !0 === c.browser.support3d ? (c.isCss3Finish = !1, !0 === b ? (c.swapSpeed("paginationSpeed"), g.setTimeout(function () { c.isCss3Finish = !0 }, c.options.paginationSpeed)) : "rewind" === b ? (c.swapSpeed(c.options.rewindSpeed), g.setTimeout(function () { c.isCss3Finish = !0 }, c.options.rewindSpeed)) : (c.swapSpeed("slideSpeed"), g.setTimeout(function () { c.isCss3Finish = !0 },
            c.options.slideSpeed)), c.transition3d(a)) : !0 === b ? c.css2slide(a, c.options.paginationSpeed) : "rewind" === b ? c.css2slide(a, c.options.rewindSpeed) : c.css2slide(a, c.options.slideSpeed); c.afterGo()
        }, jumpTo: function (a) {
            "function" === typeof this.options.beforeMove && this.options.beforeMove.apply(this, [this.$elem]); a >= this.maximumItem || -1 === a ? a = this.maximumItem : 0 >= a && (a = 0); this.swapSpeed(0); !0 === this.browser.support3d ? this.transition3d(this.positionsInArray[a]) : this.css2slide(this.positionsInArray[a], 1); this.currentItem =
            this.owl.currentItem = a; this.afterGo()
        }, afterGo: function () { this.prevArr.push(this.currentItem); this.prevItem = this.owl.prevItem = this.prevArr[this.prevArr.length - 2]; this.prevArr.shift(0); this.prevItem !== this.currentItem && (this.checkPagination(), this.checkNavigation(), this.eachMoveUpdate(), !1 !== this.options.autoPlay && this.checkAp()); "function" === typeof this.options.afterMove && this.prevItem !== this.currentItem && this.options.afterMove.apply(this, [this.$elem]) }, stop: function () { this.apStatus = "stop"; g.clearInterval(this.autoPlayInterval) },
        checkAp: function () { "stop" !== this.apStatus && this.play() }, play: function () { var a = this; a.apStatus = "play"; if (!1 === a.options.autoPlay) return !1; g.clearInterval(a.autoPlayInterval); a.autoPlayInterval = g.setInterval(function () { a.next(!0) }, a.options.autoPlay) }, swapSpeed: function (a) { "slideSpeed" === a ? this.$owlWrapper.css(this.addCssSpeed(this.options.slideSpeed)) : "paginationSpeed" === a ? this.$owlWrapper.css(this.addCssSpeed(this.options.paginationSpeed)) : "string" !== typeof a && this.$owlWrapper.css(this.addCssSpeed(a)) },
        addCssSpeed: function (a) { return { "-webkit-transition": "all " + a + "ms ease", "-moz-transition": "all " + a + "ms ease", "-o-transition": "all " + a + "ms ease", transition: "all " + a + "ms ease" } }, removeTransition: function () { return { "-webkit-transition": "", "-moz-transition": "", "-o-transition": "", transition: "" } }, doTranslate: function (a) {
            return {
                "-webkit-transform": "translate3d(" + a + "px, 0px, 0px)", "-moz-transform": "translate3d(" + a + "px, 0px, 0px)", "-o-transform": "translate3d(" + a + "px, 0px, 0px)", "-ms-transform": "translate3d(" +
                a + "px, 0px, 0px)", transform: "translate3d(" + a + "px, 0px,0px)"
            }
        }, transition3d: function (a) { this.$owlWrapper.css(this.doTranslate(a)) }, css2move: function (a) { this.$owlWrapper.css({ left: a }) }, css2slide: function (a, b) { var e = this; e.isCssFinish = !1; e.$owlWrapper.stop(!0, !0).animate({ left: a }, { duration: b || e.options.slideSpeed, complete: function () { e.isCssFinish = !0 } }) }, checkBrowser: function () {
            var a = k.createElement("div"); a.style.cssText = "  -moz-transform:translate3d(0px, 0px, 0px); -ms-transform:translate3d(0px, 0px, 0px); -o-transform:translate3d(0px, 0px, 0px); -webkit-transform:translate3d(0px, 0px, 0px); transform:translate3d(0px, 0px, 0px)";
            a = a.style.cssText.match(/translate3d\(0px, 0px, 0px\)/g); this.browser = { support3d: null !== a && 1 === a.length, isTouch: "ontouchstart" in g || g.navigator.msMaxTouchPoints }
        }, moveEvents: function () { if (!1 !== this.options.mouseDrag || !1 !== this.options.touchDrag) this.gestures(), this.disabledEvents() }, eventTypes: function () {
            var a = ["s", "e", "x"]; this.ev_types = {}; !0 === this.options.mouseDrag && !0 === this.options.touchDrag ? a = ["touchstart.owl mousedown.owl", "touchmove.owl mousemove.owl", "touchend.owl touchcancel.owl mouseup.owl"] :
            !1 === this.options.mouseDrag && !0 === this.options.touchDrag ? a = ["touchstart.owl", "touchmove.owl", "touchend.owl touchcancel.owl"] : !0 === this.options.mouseDrag && !1 === this.options.touchDrag && (a = ["mousedown.owl", "mousemove.owl", "mouseup.owl"]); this.ev_types.start = a[0]; this.ev_types.move = a[1]; this.ev_types.end = a[2]
        }, disabledEvents: function () { this.$elem.on("dragstart.owl", function (a) { a.preventDefault() }); this.$elem.on("mousedown.disableTextSelect", function (a) { return f(a.target).is("input, textarea, select, option") }) },
        gestures: function () {
            function a(a) { if (void 0 !== a.touches) return { x: a.touches[0].pageX, y: a.touches[0].pageY }; if (void 0 === a.touches) { if (void 0 !== a.pageX) return { x: a.pageX, y: a.pageY }; if (void 0 === a.pageX) return { x: a.clientX, y: a.clientY } } } function b(a) { "on" === a ? (f(k).on(d.ev_types.move, e), f(k).on(d.ev_types.end, c)) : "off" === a && (f(k).off(d.ev_types.move), f(k).off(d.ev_types.end)) } function e(b) {
                b = b.originalEvent || b || g.event; d.newPosX = a(b).x - h.offsetX; d.newPosY = a(b).y - h.offsetY; d.newRelativeX = d.newPosX - h.relativePos;
                "function" === typeof d.options.startDragging && !0 !== h.dragging && 0 !== d.newRelativeX && (h.dragging = !0, d.options.startDragging.apply(d, [d.$elem])); (8 < d.newRelativeX || -8 > d.newRelativeX) && !0 === d.browser.isTouch && (void 0 !== b.preventDefault ? b.preventDefault() : b.returnValue = !1, h.sliding = !0); (10 < d.newPosY || -10 > d.newPosY) && !1 === h.sliding && f(k).off("touchmove.owl"); d.newPosX = Math.max(Math.min(d.newPosX, d.newRelativeX / 5), d.maximumPixels + d.newRelativeX / 5); !0 === d.browser.support3d ? d.transition3d(d.newPosX) : d.css2move(d.newPosX)
            }
            function c(a) {
                a = a.originalEvent || a || g.event; var c; a.target = a.target || a.srcElement; h.dragging = !1; !0 !== d.browser.isTouch && d.$owlWrapper.removeClass("grabbing"); d.dragDirection = 0 > d.newRelativeX ? d.owl.dragDirection = "left" : d.owl.dragDirection = "right"; 0 !== d.newRelativeX && (c = d.getNewPosition(), d.goTo(c, !1, "drag"), h.targetElement === a.target && !0 !== d.browser.isTouch && (f(a.target).on("click.disable", function (a) { a.stopImmediatePropagation(); a.stopPropagation(); a.preventDefault(); f(a.target).off("click.disable") }),
                a = f._data(a.target, "events").click, c = a.pop(), a.splice(0, 0, c))); b("off")
            } var d = this, h = { offsetX: 0, offsetY: 0, baseElWidth: 0, relativePos: 0, position: null, minSwipe: null, maxSwipe: null, sliding: null, dargging: null, targetElement: null }; d.isCssFinish = !0; d.$elem.on(d.ev_types.start, ".owl-wrapper", function (c) {
                c = c.originalEvent || c || g.event; var e; if (3 === c.which) return !1; if (!(d.itemsAmount <= d.options.items)) {
                    if (!1 === d.isCssFinish && !d.options.dragBeforeAnimFinish || !1 === d.isCss3Finish && !d.options.dragBeforeAnimFinish) return !1;
                    !1 !== d.options.autoPlay && g.clearInterval(d.autoPlayInterval); !0 === d.browser.isTouch || d.$owlWrapper.hasClass("grabbing") || d.$owlWrapper.addClass("grabbing"); d.newPosX = 0; d.newRelativeX = 0; f(this).css(d.removeTransition()); e = f(this).position(); h.relativePos = e.left; h.offsetX = a(c).x - e.left; h.offsetY = a(c).y - e.top; b("on"); h.sliding = !1; h.targetElement = c.target || c.srcElement
                }
            })
        }, getNewPosition: function () {
            var a = this.closestItem(); a > this.maximumItem ? a = this.currentItem = this.maximumItem : 0 <= this.newPosX && (this.currentItem =
            a = 0); return a
        }, closestItem: function () {
            var a = this, b = !0 === a.options.scrollPerPage ? a.pagesInArray : a.positionsInArray, e = a.newPosX, c = null; f.each(b, function (d, g) {
                e - a.itemWidth / 20 > b[d + 1] && e - a.itemWidth / 20 < g && "left" === a.moveDirection() ? (c = g, a.currentItem = !0 === a.options.scrollPerPage ? f.inArray(c, a.positionsInArray) : d) : e + a.itemWidth / 20 < g && e + a.itemWidth / 20 > (b[d + 1] || b[d] - a.itemWidth) && "right" === a.moveDirection() && (!0 === a.options.scrollPerPage ? (c = b[d + 1] || b[b.length - 1], a.currentItem = f.inArray(c, a.positionsInArray)) :
                (c = b[d + 1], a.currentItem = d + 1))
            }); return a.currentItem
        }, moveDirection: function () { var a; 0 > this.newRelativeX ? (a = "right", this.playDirection = "next") : (a = "left", this.playDirection = "prev"); return a }, customEvents: function () {
            var a = this; a.$elem.on("owl.next", function () { a.next() }); a.$elem.on("owl.prev", function () { a.prev() }); a.$elem.on("owl.play", function (b, e) { a.options.autoPlay = e; a.play(); a.hoverStatus = "play" }); a.$elem.on("owl.stop", function () { a.stop(); a.hoverStatus = "stop" }); a.$elem.on("owl.goTo", function (b, e) { a.goTo(e) });
            a.$elem.on("owl.jumpTo", function (b, e) { a.jumpTo(e) })
        }, stopOnHover: function () { var a = this; !0 === a.options.stopOnHover && !0 !== a.browser.isTouch && !1 !== a.options.autoPlay && (a.$elem.on("mouseover", function () { a.stop() }), a.$elem.on("mouseout", function () { "stop" !== a.hoverStatus && a.play() })) }, lazyLoad: function () {
            var a, b, e, c, d; if (!1 === this.options.lazyLoad) return !1; for (a = 0; a < this.itemsAmount; a += 1) b = f(this.$owlItems[a]), "loaded" !== b.data("owl-loaded") && (e = b.data("owl-item"), c = b.find(".lazyOwl"), "string" !== typeof c.data("src") ?
            b.data("owl-loaded", "loaded") : (void 0 === b.data("owl-loaded") && (c.hide(), b.addClass("loading").data("owl-loaded", "checked")), (d = !0 === this.options.lazyFollow ? e >= this.currentItem : !0) && e < this.currentItem + this.options.items && c.length && this.lazyPreload(b, c)))
        }, lazyPreload: function (a, b) {
            function e() {
                a.data("owl-loaded", "loaded").removeClass("loading"); b.removeAttr("data-src"); "fade" === d.options.lazyEffect ? b.fadeIn(400) : b.show(); "function" === typeof d.options.afterLazyLoad && d.options.afterLazyLoad.apply(this,
                [d.$elem])
            } function c() { f += 1; d.completeImg(b.get(0)) || !0 === k ? e() : 100 >= f ? g.setTimeout(c, 100) : e() } var d = this, f = 0, k; "DIV" === b.prop("tagName") ? (b.css("background-image", "url(" + b.data("src") + ")"), k = !0) : b[0].src = b.data("src"); c()
        }, autoHeight: function () {
            function a() { var a = f(e.$owlItems[e.currentItem]).height(); e.wrapperOuter.css("height", a + "px"); e.wrapperOuter.hasClass("autoHeight") || g.setTimeout(function () { e.wrapperOuter.addClass("autoHeight") }, 0) } function b() {
                d += 1; e.completeImg(c.get(0)) ? a() : 100 >= d ? g.setTimeout(b,
                100) : e.wrapperOuter.css("height", "")
            } var e = this, c = f(e.$owlItems[e.currentItem]).find("img"), d; void 0 !== c.get(0) ? (d = 0, b()) : a()
        }, completeImg: function (a) { return !a.complete || "undefined" !== typeof a.naturalWidth && 0 === a.naturalWidth ? !1 : !0 }, onVisibleItems: function () {
            var a; !0 === this.options.addClassActive && this.$owlItems.removeClass("active"); this.visibleItems = []; for (a = this.currentItem; a < this.currentItem + this.options.items; a += 1) this.visibleItems.push(a), !0 === this.options.addClassActive && f(this.$owlItems[a]).addClass("active");
            this.owl.visibleItems = this.visibleItems
        }, transitionTypes: function (a) { this.outClass = "owl-" + a + "-out"; this.inClass = "owl-" + a + "-in" }, singleItemTransition: function () {
            var a = this, b = a.outClass, e = a.inClass, c = a.$owlItems.eq(a.currentItem), d = a.$owlItems.eq(a.prevItem), f = Math.abs(a.positionsInArray[a.currentItem]) + a.positionsInArray[a.prevItem], g = Math.abs(a.positionsInArray[a.currentItem]) + a.itemWidth / 2; a.isTransition = !0; a.$owlWrapper.addClass("owl-origin").css({
                "-webkit-transform-origin": g + "px", "-moz-perspective-origin": g +
                "px", "perspective-origin": g + "px"
            }); d.css({ position: "relative", left: f + "px" }).addClass(b).on("webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend", function () { a.endPrev = !0; d.off("webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend"); a.clearTransStyle(d, b) }); c.addClass(e).on("webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend", function () { a.endCurrent = !0; c.off("webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend"); a.clearTransStyle(c, e) })
        }, clearTransStyle: function (a,
        b) { a.css({ position: "", left: "" }).removeClass(b); this.endPrev && this.endCurrent && (this.$owlWrapper.removeClass("owl-origin"), this.isTransition = this.endCurrent = this.endPrev = !1) }, owlStatus: function () { this.owl = { userOptions: this.userOptions, baseElement: this.$elem, userItems: this.$userItems, owlItems: this.$owlItems, currentItem: this.currentItem, prevItem: this.prevItem, visibleItems: this.visibleItems, isTouch: this.browser.isTouch, browser: this.browser, dragDirection: this.dragDirection } }, clearEvents: function () {
            this.$elem.off(".owl owl mousedown.disableTextSelect");
            f(k).off(".owl owl"); f(g).off("resize", this.resizer)
        }, unWrap: function () { 0 !== this.$elem.children().length && (this.$owlWrapper.unwrap(), this.$userItems.unwrap().unwrap(), this.owlControls && this.owlControls.remove()); this.clearEvents(); this.$elem.attr("style", this.$elem.data("owl-originalStyles") || "").attr("class", this.$elem.data("owl-originalClasses")) }, destroy: function () { this.stop(); g.clearInterval(this.checkVisible); this.unWrap(); this.$elem.removeData() }, reinit: function (a) {
            a = f.extend({}, this.userOptions,
            a); this.unWrap(); this.init(a, this.$elem)
        }, addItem: function (a, b) { var e; if (!a) return !1; if (0 === this.$elem.children().length) return this.$elem.append(a), this.setVars(), !1; this.unWrap(); e = void 0 === b || -1 === b ? -1 : b; e >= this.$userItems.length || -1 === e ? this.$userItems.eq(-1).after(a) : this.$userItems.eq(e).before(a); this.setVars() }, removeItem: function (a) { if (0 === this.$elem.children().length) return !1; a = void 0 === a || -1 === a ? -1 : a; this.unWrap(); this.$userItems.eq(a).remove(); this.setVars() }
    }; f.fn.owlCarousel = function (a) {
        return this.each(function () {
            if (!0 ===
            f(this).data("owl-init")) return !1; f(this).data("owl-init", !0); var b = Object.create(l); b.init(a, this); f.data(this, "owlCarousel", b)
        })
    }; f.fn.owlCarousel.options = {
        items: 5, itemsCustom: !1, itemsDesktop: [1199, 4], itemsDesktopSmall: [979, 3], itemsTablet: [768, 2], itemsTabletSmall: !1, itemsMobile: [479, 1], singleItem: !1, itemsScaleUp: !1, slideSpeed: 200, paginationSpeed: 800, rewindSpeed: 1E3, autoPlay: !1, stopOnHover: !1, navigation: !1, navigationText: ["prev", "next"], rewindNav: !0, scrollPerPage: !1, pagination: !0, paginationNumbers: !1,
        responsive: !0, responsiveRefreshRate: 200, responsiveBaseWidth: g, baseClass: "owl-carousel", theme: "owl-theme", lazyLoad: !1, lazyFollow: !0, lazyEffect: "fade", autoHeight: !1, jsonPath: !1, jsonSuccess: !1, dragBeforeAnimFinish: !0, mouseDrag: !0, touchDrag: !0, addClassActive: !1, transitionStyle: !1, beforeUpdate: !1, afterUpdate: !1, beforeInit: !1, afterInit: !1, beforeMove: !1, afterMove: !1, afterAction: !1, startDragging: !1, afterLazyLoad: !1
    }
})(jQuery, window, document);
/*!rjAccordion jQuery Plugin v0.0*/
!function (n) { "use strict"; function t(i, o) { this.$element = n(i), this.options = n.extend({}, t.defaults, o) } t.prototype.constructor = t, t.defaults = { transition_delay: 300, toggle: !0 }, t.prototype.init = function () { function t() { e.find(".accordion-section-header").removeClass("active"), e.find(".accordion-section-content").slideUp(c.transition_delay).removeClass("open") } function i(n) { n.find(".accordion-section-header").removeClass("active"), n.find(".accordion-section-content").slideUp(c.transition_delay).removeClass("open") } function o(n) { var t = n.data("target"); n.addClass("active"), e.find(t).slideDown(c.transition_delay).addClass("open") } var e = this.$element, c = this.options; e.find(".accordion-section-header").click(function (e) { e.preventDefault(), c.toggle ? n(e.target).is(".active") ? t() : (t(), o(n(this))) : n(e.target).is(".active") ? i(n(this).closest(".accordion-section")) : o(n(this)) }) }; var i = n.fn.rjAccordion; n.fn.rjAccordion = function (n) { return this.each(function () { var i = new t(this, n); i.init() }) }, n.fn.rjAccordion.constructor = t, n.fn.rjAccordion.noConflict = function () { return n.fn.rjAccordion = i, this } }(window.jQuery);