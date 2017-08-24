import { Injectable, Inject, EventEmitter } from "@angular/core";
import { Http } from "@angular/http";
import { BaseService } from "./base.service";
import { AddComment } from "../../comments/add-comment";
import { AuthService } from "./auth.service";

@Injectable()
export class CommentsService extends BaseService {
    public commentsMustUpdate: EventEmitter<boolean> = new EventEmitter();

    constructor(private _http: Http,
        private _authService: AuthService,
        @Inject('ORIGIN_URL') private originUrl: string) {
        super();
    }

    public getPostComments(id: number): any {
        return this._http.get(this.originUrl + '/api/comments/forpost?postId=' + id);
    }

    public addComment(type: string, commentModel: AddComment): any {
        return this._http.post(`${this.originUrl}/api/comments/addto${type}`, commentModel, this._authService.getAuthTokenHeaders())
            .map(res => {
                this.commentsMustUpdate.emit(true);
                return true; 
            })
            .catch(this.handleError);
    }
}
