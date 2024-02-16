import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { StudentModel } from '../../models/student.model';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { StudentPipe } from '../../pipes/student.pipe';
import { FormValidateDirective } from 'form-validate-angular';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { ClassRoomModel } from '../../models/class-room.model';
import { InfiniteScrollModule } from "ngx-infinite-scroll";
import { RequestModel } from '../../models/request.model';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule, StudentPipe, FormValidateDirective, InfiniteScrollModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  classRooms: ClassRoomModel[] = [];
  students: StudentModel[] = [];
  @ViewChild("studentAddModalCloseBtn") studentAddModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("classRoomAddModalCloseBtn") classRoomAddModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  addStudentModel: StudentModel = new StudentModel();
  addClassRoomModel: ClassRoomModel = new ClassRoomModel();
  updateStudentModel: StudentModel = new StudentModel()
  selectedRoomId: string = "";
  request: RequestModel = new RequestModel()
  isLoadingMore: boolean = false
  constructor(
    private http: HttpService,
    private swal: SwalService
  ) {
    this.getAllClassRooms();
  }

  feedData() {
    if (this.isLoadingMore === false) {
      this.isLoadingMore = true;
      this.request.pageSize += 50;
      this.request.pageNumber += 50;
      this.getAllStudentsByClassRoomId(this.selectedRoomId)
    }
  }

  getAllClassRooms() {
    this.http.get("ClassRooms/GetAll", (res) => {
      this.classRooms = res;
      if (this.classRooms.length > 0) {
        this.getAllStudentsByClassRoomId(this.classRooms[0].id);

      }
    });
  }

  getAllStudentsByClassRoomId(roomId: string) {
    if (this.selectedRoomId !== roomId) {
      this.clearData();
    }
        this.selectedRoomId = roomId;
        this.request.classRoomId = this.selectedRoomId
        this.http.post("Students/GetAllByClassRoomId", this.request, (res) => {
        this.students = this.students.concat(res);
        this.isLoadingMore = false;
    });

  }
  searchStudent(searchTerm: string) {
    this.request.search = searchTerm
    if (this.request.search.length > 3) {
      this.clearData();
      this.getAllStudentsByClassRoomId(this.selectedRoomId)
    }
    if (this.request.search.length == 0) {
      this.clearData();
      this.getAllStudentsByClassRoomId(this.selectedRoomId)
    }
  }
  createStudent(form: NgForm) {
    if (form.valid) {
      if (this.addStudentModel.classRoomId === "0") {
        alert("You have choose a valid class room");
        return;
      }

      this.http.post("Students/Create", this.addStudentModel, (res) => {
        this.studentAddModalCloseBtn?.nativeElement.click();
        this.swal.callToast(res.message);
        this.getAllStudentsByClassRoomId(this.addStudentModel.classRoomId);
      });
    }
  }
  clearData() {
    this.request.pageSize = 50;
    this.request.pageNumber = 0;
    this.students = [];
  }
  clearAddStudentModel() {
    this.addStudentModel = new StudentModel();
    const inputs = document.querySelectorAll(".form-control.is-invalid");
    for (let i in inputs) {
      const el = inputs[i];
      el.classList.remove("is-invalid");
    }
  }

  createClassRoom(form: NgForm) {
    if (form.valid) {
      this.http.post("ClassRooms/Create", this.addClassRoomModel, (res) => {
        this.classRoomAddModalCloseBtn?.nativeElement.click();
        this.swal.callToast(res.message);
      });
    }
  }

  clearAddClassRoomModel() {
    this.addClassRoomModel = new ClassRoomModel();
    const inputs = document.querySelectorAll(".form-control.is-invalid");
    for (let i in inputs) {
      const el = inputs[i];
      el.classList.remove("is-invalid");
    }
  }
}
