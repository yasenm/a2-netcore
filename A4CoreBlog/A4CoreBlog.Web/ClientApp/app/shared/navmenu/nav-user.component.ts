import { Component, OnChanges, OnInit, SimpleChanges } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { UserInterface } from "../../users/user";
import { ProfileService } from "../services/profile.service";

@Component({
    selector: 'nav-user',
    templateUrl: './nav-user.component.html',
    styleUrls: ['./nav-user.component.css']
})
export class NavUserComponent implements OnInit {
    user: UserInterface;
    loggedIn: boolean;

    constructor(private _authService: AuthService,
        private _profileService: ProfileService) { }

    ngOnInit(): void {
        this.loggedIn = this._authService.isLoggedIn();
        this.getProfile();
        this.eventsSubribing();
    }

    logout(): void {
        this._authService.logout();
    }

    getProfile(): void {
        if (this.loggedIn) {
            this._profileService.getProfileOfCurrentUser()
                .subscribe(res => {
                    this.user = res.json() as UserInterface;
                }, err => {

                });
        } else {
            this.user = null;
        }
    }

    private eventsSubribing(): void {
        this._authService.logginStateChange.subscribe(
            value => {
                this.loggedIn = value;
                this.getProfile();
            })
    }
}