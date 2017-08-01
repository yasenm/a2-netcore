import { Observable } from "rxjs/Observable";

export abstract class BaseService {

    protected handleError(error: any) {
        return Observable.throw('Server error');
    }
}