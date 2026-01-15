import { reactive } from "vue";

export const notification = reactive({
  show: false,
  type: "info", // success | error | info
  message: ""
});

export function notifySuccess(msg) {
  notification.show = true;
  notification.type = "success";
  notification.message = msg;
}

export function notifyError(msg) {
  notification.show = true;
  notification.type = "error";
  notification.message = msg;
}

export function notifyInfo(msg) {
  notification.show = true;
  notification.type = "info";
  notification.message = msg;
}

export function clearNotification() {
  notification.show = false;
  notification.message = "";
}

export function zapisnikIzabran(){
  notification.show = true;
  notification.type = "info";
  notification.message = "Zapisnik je otvoren.";
}

export function studentIzabran(){
  notification.show = true;
  notification.type = "info";
  notification.message = "Student je izabran.";
}