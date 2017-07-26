import { Component, OnInit } from '@angular/core';

import { UserInterface } from "./user";
import { UsersService } from "../shared/services/users.service";

@Component({
    selector: 'users-sm-list',
    templateUrl: "./users-sm-list.component.html",
    styleUrls: ['./users-sm-list.component.css'],
    providers: [UsersService]
})
export class UsersSmListComponent implements OnInit {
    public users: UserInterface[];
    
    constructor(private _usersService: UsersService){}

    ngOnInit(): void {
        this._usersService.getUsers()
            .subscribe((data) => this.users = data.json() as UserInterface[]);
    }
}