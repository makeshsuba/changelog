import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ILog, ILogList, ILogType } from '../model/log.model';
import { environment } from '../../environments/environment';



@Injectable({ providedIn: 'root' })

export class LogService {
  constructor(private httpClient: HttpClient) { }
  apiUrl = environment.apiUrl;

  getLog(logId: number) {
    return this.httpClient.get<ILog>(this.apiUrl + 'logs/GetLog?logId=' + logId);
  }

  getLogs(userName: string) {
    return this.httpClient.get<ILogList>(this.apiUrl + 'logs/GetLogs?userName=' + userName);
  }

  addLog(log: ILog) {
    return this.httpClient.post<any>(this.apiUrl + 'logs/AddLog', log);
  }

  updateLog(log: ILog) {
    return this.httpClient.put<any>(this.apiUrl + 'logs/UpdateLog', log);
  }

  deleteLog(logId: number) {
    return this.httpClient.delete<any>(this.apiUrl + 'logs/DeleteLog?logId=' + logId);
  }

  getLogTypes() {
    return this.httpClient.get<ILogType>(this.apiUrl + 'logs/GetLogTypes');
  }


}
