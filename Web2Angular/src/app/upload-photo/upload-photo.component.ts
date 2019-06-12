import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ServerConnectionService } from '../server-connection.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-upload-photo',
  templateUrl: './upload-photo.component.html',
  styleUrls: ['./upload-photo.component.css']
})
export class UploadPhotoComponent implements OnInit {

  fileToUpload: File = null;

  uploadPhotoForm = this.fb.group({
    Picture: ['']
  });

  selectedFile: File = null;
  /*onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
  }*/
  

  constructor(private fb: FormBuilder,private router: Router, private ServerConnectionService: ServerConnectionService, private http: HttpClient) { }

  ngOnInit() {
  }


  handleFileInput(files: FileList) 
  {
    this.fileToUpload = files.item(0);
  }

  /*uploadPhoto() {
    let formData: FormData = new FormData();

    let options = {
      sheaders:
      {
        "Content-type": "application/json",
      }
    }

    if (this.selectedFile != null) {
      formData.append('Picture', this.selectedFile, this.selectedFile.name);

      this.ServerConnectionService.uploadPhotoToBackend(this.uploadPhotoForm, localStorage.getItem("name"), options).subscribe(data => {
        
        this.router.navigate(["/login"]);
      },
        err => {
          console.log(err);
        }
      )
    }
  }*/

  onSubmit() {

            this.uploadFileToActivity();
  }

uploadFileToActivity() {
    let e = localStorage.getItem("name");
    this.ServerConnectionService.postFile(this.fileToUpload, e).subscribe(data => {
    }, error => {
      console.log(error);
    });
  }
  
}
