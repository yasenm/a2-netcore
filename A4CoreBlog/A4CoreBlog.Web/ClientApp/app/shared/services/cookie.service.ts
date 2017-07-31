import { Injectable } from "@angular/core";

@Injectable()
export class CookieService {

    public setCookie(cookieName: string, cookieValue: string, cookieExp: string): void {
        var expires = "expires=" + cookieExp;
        document.cookie = cookieName + "=" + cookieValue + ";" + expires + ";path=/";
    }

    public getCookie(cookieName: string): string {
        var name = cookieName + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }

    public deleteCookie(cookieName: string): void {
        this.setCookie(cookieName, "", "Thu, 01 Jan 1970 00:00:01 GMT");
    }
}