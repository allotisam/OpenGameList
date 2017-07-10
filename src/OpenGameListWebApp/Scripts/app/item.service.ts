import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptions } from "@angular/http";
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

    add(item: Item) {
        var url = this.baseUrl;

        return this.http.post(url, JSON.stringify(item), this.getRequestOptions())
            .map(response => response.json())
            .catch(this.handleError);
    }

    update(item: Item) {
        var url = this.baseUrl + item.Id;

        return this.http.put(url, JSON.stringify(item), this.getRequestOptions())
            .map(response => response.json())
            .catch(this.handleError);
    }

    delete(id: number) {
        var url = this.baseUrl + id;

        return this.http.delete(url)
            .catch(this.handleError);
    }

    private getRequestOptions() {
        return new RequestOptions({
            headers: new Headers({
                "Content-Type": "application/json"
            })
        });
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || "Server Error");
    }
}