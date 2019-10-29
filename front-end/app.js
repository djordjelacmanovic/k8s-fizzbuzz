class App {
  constructor(document) {
    this.document = document;
  }

  async getAndShowNextValue() {
    const { value, app_name, app_version } = await this._getNextValue();
    this.fizzBuzzValue = value;
    this.appName = app_name;
    this.appVersion = app_version;
    this.showFizzBuzzElement();
    this.hideMessageElement();
  }

  async resetValueAndView() {
    await this._resetValue();

    this.message = 'Counter reset!';
    this.fizzBuzzValue = null;
    this.appName = null;
    this.appVersion = null;

    this.showMessageElement();
    this.hideFizzBuzzElement();
  }

  showFizzBuzzElement(){
    this.fizzbuzzElement.style.display = 'contents';
  }

  hideFizzBuzzElement(){
    this.fizzbuzzElement.style.display = 'none';
  }

  showMessageElement(){
    this.messageElement.style.display = 'contents';
  }

  hideMessageElement(){
    this.messageElement.style.display = 'none';
  }

  set message(message){
    this._message = message;
    this.messageElement.textContent = this._message;
  }
  
  set fizzBuzzValue(value){
    this._fizzBuzzValue = value;
    this.document.getElementById('fizzbuzz-value').textContent = this._fizzBuzzValue;
  }

  set appName(name){
    this._appName = name;
    this.document.getElementById('app-name').textContent = this._appName;
  }

  set appVersion(version){
    this._appVersion = version;
    this.document.getElementById('app-version').textContent = this._appVersion;
  }

  get messageElement(){
    return this.document.getElementById('message');
  }

  get fizzbuzzElement(){
    return this.document.getElementById('fizzbuzz');
  }

  async _getNextValue() {
    try {
      return await this._ajax('api/v1/fizzbuzz');
    } catch (error) {
      console.error(error);
      this.message = `Error: ${error.message}`;
      this.showMessageElement();
    }
  }

  async _resetValue() {
    try {
      await this._ajax('api/v1/fizzbuzz', 'DELETE');
    } catch (error) {
      console.error(error);
      this.message = `Error: ${error.message}`;
      this.showMessageElement();
    }
  }

  _ajax(path, method = 'GET', payload) {
    return new Promise((resolve, reject) => {
      const r = new XMLHttpRequest();
      r.open(method, `http://localhost:8080/${path}`, true);
      r.setRequestHeader('Accept', 'application/json');
      if(payload) r.setRequestHeader('Content-Type', 'application/json');
      r.onreadystatechange = () => {
        if (r.readyState != 4) return;
        if (r.status > 299) return reject(r.response);
        return resolve(r.status != 204 ? JSON.parse(r.response) : null);
      };
      payload ? r.send(JSON.stringify(payload)) : r.send();
    });
  }
}

const app = new App(document);
window.getAndShowNextValue = () => app.getAndShowNextValue();
window.resetValueAndView = () => app.resetValueAndView();