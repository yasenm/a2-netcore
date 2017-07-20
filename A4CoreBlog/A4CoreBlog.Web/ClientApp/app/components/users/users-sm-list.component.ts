import { Component, OnInit } from '@angular/core';

import { UsersService } from '../../services/users.service';
import { UserInterface } from './user-list';

@Component({
    selector: 'users-sm-list',
    templateUrl: "./users-sm-list.component.html",
    providers: [UsersService]
})
export class UsersSmListComponent implements OnInit {
    public title: string;
    public users: UserInterface[];
    
    constructor(private _usersService: UsersService){}

    ngOnInit(): void {
        this._usersService.getUsers()
            .subscribe((data) => this.users = data.json() as UserInterface[]);
    }
}