import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { Item } from "./item";
import "rxjs/Rx";

@Injectable()
export class ItemService {
    constructor(private http: Http) { }

    private baseUrl = "api/items/";

    private handleError(error: Response) {
        // output errors to the console.
        console.error(error);
        return Observable.throw(error.json().error || "Server error");
    }
    getLatest(num?: number) {
        let url = this.baseUrl + "GetLatest/";
        if (num!=null) {
            url += num;
        }
        return this.http.get(url)
            .map((response:Response) => response.json())
            .catch(this.handleError);
    }

    getMostViewed(num?: number) {
        let url = this.baseUrl + "GetMostViewed/";
        if (num != null) {
            url += num;
        }
        return this.http.get(url)
            .map((response: Response) => response.json())
            .catch(this.handleError);
    }

    getRandom(num?:number) {
        let url = this.baseUrl + "GetRandom/";
        if (num != null) {
            url += num;
        }
        return this.http.get(url)
            .map((response: Response) => response.json())
            .catch(this.handleError);
    }

    get(id: number) {
        if (id == null) { throw new Error("Id required") };
        const url = this.baseUrl + id;
        return this.http.get(url)
            .map((response: Response) => response.json())
            .catch(this.handleError);
    }
}