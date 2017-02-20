System.register(["@angular/core", "@angular/forms", "@angular/router"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, forms_1, router_1;
    var LoginComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (forms_1_1) {
                forms_1 = forms_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            }],
        execute: function() {
            LoginComponent = (function () {
                function LoginComponent(fb, router) {
                    this.fb = fb;
                    this.router = router;
                    this.title = "Login";
                    this.loginForm = null;
                    this.loginForm = fb.group({
                        username: ["", forms_1.Validators.required],
                        password: ["", forms_1.Validators.required]
                    });
                }
                LoginComponent.prototype.performLogin = function (e) {
                    e.preventDefault();
                    alert(JSON.stringify(this.loginForm.value));
                };
                LoginComponent = __decorate([
                    core_1.Component({
                        selector: "login",
                        template: "\n        <div class=\"login-container\">\n            <h2 class=\"form-login-heading\">Login</h2>\n            <div class=\"alert alert-danger\" role=\"alert\" *ngIf=\"loginError\">\n                <strong>Warning:</strong> Username or Password mismatch\n            </div>\n            <form class=\"form-login\" [formGroup]=\"loginForm\" (ngSubmit)=\"performLogin($event)\">\n                <input [formControl]=\"username\" type=\"text\" class=\"form-control\" placeholder=\"Your username or e-mail address\" required autofocus />\n                <input [formControl]=\"password\" type=\"password\" class=\"form-control\" placeholder=\"Your password\" required />\n                <div class=\"checkbox\">\n                    <label>\n                        <input type=\"checkbox\" value=\"remember-me\"> Remember Me\n                    </label>\n                </div>\n                <button class=\"btn btn-lg btn-primary btn-block\" typpe=\"submit\">Sign in</button>\n            </form>\n        </div>\n    "
                    }), 
                    __metadata('design:paramtypes', [forms_1.FormBuilder, router_1.Router])
                ], LoginComponent);
                return LoginComponent;
            }());
            exports_1("LoginComponent", LoginComponent);
        }
    }
});
