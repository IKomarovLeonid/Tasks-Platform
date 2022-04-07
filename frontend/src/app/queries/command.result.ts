export class CommandResult<T>{

  constructor(public readonly data: T | undefined, public readonly errorMessage = "") {
  }

  public isSuccess(): boolean{
    return this.errorMessage == "";
  }
}
