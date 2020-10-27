import { debugOutputAstAsTypeScript } from '@angular/compiler';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ILog, ILogType } from '../model/log.model';
import { AuthService } from '../services/auth-service';
import { LogService } from '../services/log-service';

@Component({
  selector: 'app-modal-content',
  templateUrl: './modal-content.component.html',
  styleUrls: ['./modal-content.component.scss']
})

export class ModalContentComponent implements OnInit, OnDestroy {
  @Input() logId: number;
  @Output() notifyParent: EventEmitter<any> = new EventEmitter();
  modalFormGroup: FormGroup;
  logType: ILogType;
  submitted = false;
  log: ILog;
  private readonly onDestroy$ = new Subject<void>();

  constructor(
    public activeModal: NgbActiveModal, private authService: AuthService,
    private formBuilder: FormBuilder, private logservice: LogService) { }

  ngOnInit() {
    this.loadLogTypes();
    this.defaultLog();
    this.buildLogForm();
    if (this.logId) { this.loadLog(this.logId); }
  }

  defaultLog() {
    this.log = {
      id: 0,
      userName: '',
      logDescription: '',
      logTitle: '',
      logTypeId: 0,
      logType: '',
      chipColor: '',
      createdDate: new Date(Date.now())
    };
  }

  get f() { return this.modalFormGroup.controls; }

  buildLogForm() {
    this.modalFormGroup = this.formBuilder.group({
      id: this.log.id,
      userName: this.log.userName,
      logTitle: [this.log.logTitle, Validators.required],
      logDescription: [this.log.logDescription, [Validators.required]],
      logTypeId: [this.log.logTypeId, [Validators.required, Validators.min(1)]],
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.modalFormGroup.invalid) {
      return;
    }
    if (!this.logId) {
      this.addLog();
    } else {
      this.updateLog();
    }
  }

  addLog() {
    this.log = this.modalFormGroup.value;
    this.log.userName = this.authService.userName;
    this.log.logTypeId = +this.modalFormGroup.controls.logTypeId.value;
    this.logservice.addLog(this.log).pipe(takeUntil(this.onDestroy$))
      .subscribe(() => {
        this.notifyParent.emit('New log has been added succssfully');
        this.activeModal.close('Submit');
      });
  }

  updateLog() {
    this.log = this.modalFormGroup.value;
    this.log.userName = this.authService.userName;
    this.log.logTypeId = +this.modalFormGroup.controls.logTypeId.value;
    this.logservice.updateLog(this.log).pipe(takeUntil(this.onDestroy$))
      .subscribe(() => {
        this.notifyParent.emit('The log has been updated succssfully');
        this.activeModal.close('Submit');
      });
  }

  loadLog(logId: number) {
    this.logservice.getLog(logId).pipe(takeUntil(this.onDestroy$))
      .subscribe((data) => {
        this.log = data;
        this.buildLogForm();
      });
  }

  loadLogTypes() {
    this.logservice.getLogTypes().subscribe((data) => {
      this.logType = data;
    });
  }

  ngOnDestroy() {
    this.onDestroy$.next();
  }

}
