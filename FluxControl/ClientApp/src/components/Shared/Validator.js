
export const FormValidator = (input) => {
  if (input.required) {
    if (input.value != null) {
      if (input.value.length === 0) {
        return `Campo Requerido`;
      }

      if (input.select && input.value === "default") {
        return `Campo Requerido`;
      }
    } else return `Campo Requerido`;
  }

  if (input.minlength) {
    if (input.value.length < input.minlength) {
      return `Deve conter ao menos ${input.minlength} caracteres`;
    }
  }

  if (input.email) {
    const pattern = new RegExp(/(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])/g);
    if (!pattern.test(input.value)) {
      return `Insira um email válido`;
    }
  }

  return null;
}

export const FormLimiter = (input, newInput = null) => {
  let value = input.value;

  if (input.onlyNumber) {
     value = input.value.replace(/\D/g, '');
  }

  return value;
}