import { Component, Inject } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';


@Component({
    selector: 'books',
    templateUrl: './books.component.html'
})

export class BooksComponent {
    public books: BookList[];
    http: Http;
    baseurl: string;
    book: BookList;
    model: BookList;
    isEdit: boolean;
    loading = false;
    total = 0;
    page = 1;
    limit = 5;


    constructor(_http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.http = _http;
        this.baseurl = baseUrl;
        this.refreshBooks();
        this.isEdit = false;
    }

    refreshBooks() {
        this.http.get(this.baseurl + 'api/SampleData/BookDetails?page=' + this.page + "&limit=" + this.limit).subscribe(res => {
           // this.books = res.json() as BookList[];
            var dataresult = res.json();
            this.total = dataresult.total;
            this.books = dataresult.books as BookList[];
            this.loading = false;

        }, error => console.error(error));
    }

    submitted = false;

    editBook(book: BookList) {
        this.book = book;
        this.isEdit = true;
    }

    cancelEdit() {
        this.isEdit = false;
    }

    onSubmit() {
        this.submitted = true;
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = JSON.stringify(this.book);
        this.http.post(this.baseurl + 'api/SampleData/UpdateBookDetails', body, options).subscribe((res: Response) => res.json());
    }
    goToPage(n: number): void {
        this.page = n;
        this.refreshBooks();
    }

    onNext(): void {
        this.page++;
        this.refreshBooks();
    }

    onPrev(): void {
        this.page--;
        this.refreshBooks();
    }

}

export class BookList {

    constructor(
        public name: string,
        public authors: string,
        public numOfPages: string,
        public dateOfPublication: string
    ) {}
}