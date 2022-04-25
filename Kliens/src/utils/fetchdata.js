const api = "https://szavazo-rendszer.herokuapp.com" + "/api/";

export async function GetData(params) {
  console.log("Params:", params);
  console.log("Calling: ", api + params);
  const response = await fetch(api + params);
  const data = await response.json();

  return data;
}

export function PushData(params) {}

export function DeleteData(params) {}

export function UpdateData(params) {}

export function SubmitForm(data) {
  fetch(api + "SubmittedForms", data)
    .then((response) => response.text())
    .then((result) => console.log(result))
    .catch((error) => console.log("error", error));
}
