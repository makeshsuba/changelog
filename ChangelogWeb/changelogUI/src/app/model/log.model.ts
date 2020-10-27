export interface ILog {
  id: number;
  userName: string;
  logTitle: string;
  logType: string;
  logTypeId: number;
  logDescription: string;
  chipColor: string;
  createdDate: Date;
}

export interface ILogList {
  logsData: ILog[];
}

export interface ILogType {
  logTypeId: number;
  logType: string;
}
