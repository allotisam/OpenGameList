import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { Item } from "./item";

@Injectable()
export class ItemService {

    private baseUrl = "api/items/";

    constructor(private http: Http) { }

    getLatest(num?: number) {
        var url = this.baseUrl + "GetLatest/";
        if (num != null) {
            url += num;
        }

        return this.http.get(url)
            .map(response => response.json())
            .catch(this.handleError);
    }

    getMostViewed(num?: number) {
        var url = this.baseUrl + "GetMostViewed/";
        if (num != null) {
            url += num;
        }

        return this.http.get(url)
            .map(response => response.json())
            .catch(this.handleError);
    }

    getRandom(num?: number) {
        var url = this.baseUrl + "GetRandom/";
        if (num != null) {
            url += num;
        }

        return this.http.get(url)
            .map(response => response.json())
            .catch(this.handleError);
    }

    get(id: number) {
        if (id == null) {
            throw new Error("id is required");
        }

        var url = this.baseUrl + id;
        return this.http.get(url).map(response => <Item>response.json()).catch(this.handleError);
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || "Server Error");
    }
}