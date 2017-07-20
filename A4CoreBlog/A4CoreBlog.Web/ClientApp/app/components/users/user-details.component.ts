﻿import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";

import { UserInterface } from './user-list';
import { UsersService } from '../../services/users.service';

import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'user-details',
    templateUrl: 'user-details.component.html',
    providers: [UsersService]
})
export class UserDetailsComponent implements OnInit {
    public user: UserInterface;

    constructor(private _router: Router,
        private _route: ActivatedRoute,
        private _usersService: UsersService)
    { }
    
    ngOnInit(): void {
        this._route.params.subscribe(params =>
            this._usersService.getUser(params["username"])
                .subscribe(data => this.user = data.json() as UserInterface));
    }
}