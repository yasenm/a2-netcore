import { Observable } from "rxjs/Observable";
import 'rxjs/add/observable/throw';

export abstract class BaseService {
    protected handleError(error: any) {
        let errors: string[] = new Array();
        for (let err in error.json()) {
            errors.push(error.json()[err][0])
        }
        return Observable.throw(errors);
    }
}