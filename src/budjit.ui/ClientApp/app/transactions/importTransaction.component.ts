import { Component, ViewChild, ElementRef, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, FormControl, Validators } from "@angular/forms";
import { DataService } from "../shared/dataService";
import { HttpErrorResponse } from "@angular/common/http/src/response";
import { ExpansionCase } from "@angular/compiler";

@Component({
    selector: "import-transaction",
    templateUrl: "importTransaction.component.html"
})
export class ImportTransaction implements OnInit {
    title = "Upload";

    fileUploadForm: FormGroup;
    loading: boolean = false;

    errorMessage: string;
    successMessage: string;

    constructor(private formBuilder: FormBuilder, private data: DataService) { }

    @ViewChild("transactionsFileInput") transactionsFileInput: any;

    ngOnInit() {
        this.fileUploadForm = this.formBuilder.group({
            'transactionsFile': [null, Validators.required]
        });
    }

    onFileChange(event: any) {
        this.errorMessage = "";
        this.successMessage = "";

        let fileInput = this.transactionsFileInput.nativeElement;

        if (fileInput.files && fileInput.files[0]) {
            this.fileUploadForm.setValue({ "transactionsFile": fileInput.files[0] })
        }
    }

    private prepareSave(): any {
        let input = new FormData();
        let file = this.fileUploadForm.get('transactionsFile');

        if (file == null) {
            this.errorMessage = "You must provide a file to upload";
            throw new Error("file is null");
        }

        input.append('file', file.value);
        return input;
    }

    onSubmit() {
        const formModel = this.prepareSave();
        this.loading = true;

        this.data.uploadTransactionFile(formModel)
            .subscribe(
            res => {
                this.successMessage = "File successfully uploaded."
                this.clearFile();
            },
            (err: HttpErrorResponse) => {
                this.errorMessage = "Unable to upload file. Please try again later."
            },
            () => this.loading = false
            );
    }

    clearFile() {

        this.fileUploadForm.reset({
            transactionsFile: null
        });

        this.transactionsFileInput.nativeElement.value = '';
    }
}