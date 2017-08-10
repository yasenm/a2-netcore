import { Component, OnChanges, OnInit, SimpleChanges } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { UserInterface } from "../../users/user";

@Component({
    selector: 'nav-user',
    templateUrl: './nav-user.component.html',
    styleUrls: ['./nav-user.component.css']
})
export class NavUserComponent implements OnInit {
    user: UserInterface;
    loggedIn: boolean;

    constructor(private _authService: AuthService) { }

    ngOnInit(): void {
        this.loggedIn = this._authService.isLoggedIn();
        this._authService.logginStateChange.subscribe(
            value => {
                this.loggedIn = value;
            })
    }

    logout(): void {
        this._authService.logout();
    }
}