import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ModalDismissReasons, NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { map } from 'rxjs/internal/operators/map';
import { ILog } from '../model/log.model';
import { ChipColor } from '../model/user.register.model';
import { AuthService } from '../services/auth-service';
import { LogService } from '../services/log-service';
import { ToastrService } from 'ngx-toastr';
import { ModalContentComponent } from '../modal-content/modal-content.component';


@Component({
  selector: 'app-log-list',
  templateUrl: './log-list.component.html',
  styleUrls: ['./log-list.component.scss']
})
export class LogListComponent implements OnInit {
  logs: ILog[];
  @Input() logId: number;
  availableColors: ChipColor[] = [
    { name: 'Bug', color: '#e32636ba' },
    { name: 'Update', color: '#009e49ab' },
    { name: 'Feature', color: '#4169e1d6' },
  ];

  ngbModalOptions: NgbModalOptions = {
    backdrop: 'static',
    keyboard: false
  };

  closeResult: string;
  constructor(
    private logService: LogService, private authService: AuthService, private modalService: NgbModal,
    private router: Router, private changeDetect: ChangeDetectorRef, private toastr: ToastrService) { }

  getNotification(event) {
    this.loadLogs();
  }

  open(logId = 0) {
    const modalRef = this.modalService.open(ModalContentComponent, this.ngbModalOptions);
    modalRef.componentInstance.logId = logId;
    modalRef.componentInstance.notifyParent.subscribe((result) => {
      this.toastr.success(result);
      this.loadLogs();
    });
  }

  ngOnInit(): void {
    if (!this.authService.userName) {
      this.logOut();
    } else {
      this.loadLogs();
    }
  }

  logOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('socialusers');
    this.router.navigate(['']);
  }

  loadLogs() {
    this.logService.getLogs(this.authService.userName).pipe(map(data => {
      if (data.logsData.length >= 1) {
        data.logsData.forEach(element => {
          const color = this.availableColors.find(i => i.name === element.logType).color;
          element.chipColor = color;
        });
        this.logs = data.logsData;
        this.changeDetect.detectChanges();
      }
      else {
        this.logs = [];
      }
    })
    ).subscribe();
  }

  editLog(logId: number) {
    this.open(logId);
  }

  deleteLog(logId: number) {
    this.logService.deleteLog(logId).subscribe(() => {
      this.toastr.success('Deleted successfully');
      this.loadLogs();
    });
  }

}
