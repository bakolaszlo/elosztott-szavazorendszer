<template>
  <div class="kerdes">
    <Transition>
      <div v-if="loaded">
        <FormKit
          v-if="loaded"
          type="form"
          help="Kérdés beküldése"
          @submit="handleForm"
          submit-label="Kérdés küldése"
          v-model="formValues"
          name="mainForm"
        >
          <FormKit
            v-model="value"
            type="text"
            label="Adja meg a kérdését"
            help="Irja be a kérdést!"
            validation="required|length:1,60"
            name="question"
          />
          <FormKit
            v-model="customAnswers"
            type="radio"
            label="Szeretne modosítani az alap válaszokon?"
            :options="['Alap', 'Checkbox', 'Szöveg', 'Rádió']"
            name="questionType"
          />
          <Transition>
            <div v-if="customAnswers == 'Checkbox' || customAnswers == 'Rádió'">
              <div class="text-center">Adja meg a válasz lehetőségeket:</div>
              <div class="buttons">
                <div @click="answerCounter -= 1" class="btn btn-danger">-1</div>
                <div @click="answerCounter += 1" class="btn btn-success">
                  +1
                </div>
              </div>

              <div
                v-for="index in answerCounter"
                :key="index"
                class="form-margin-top"
              >
                <FormKit
                  :key="index"
                  type="text"
                  label=""
                  :help="index + ' válasz lehetőség'"
                  :name="'answer_' + index"
                />
              </div>
            </div>
          </Transition>
        </FormKit>
      </div>
    </Transition>
    <Transition>
      <div v-if="isLoading">
        <input type="checkbox" id="check" :checked="submitSuccess" />
        <label for="check" class="parameter">
          <div class="check-icon"></div>
        </label>
      </div>
    </Transition>
    <Transition>
      <h1 class="succes" v-if="showSuccesfullMessage && !loaded">
        Sikeresen regisztráltuk a kérdést. <br />
        <Router-link :to="'/eredmenyek/' + formId">Itt</Router-link>
        követheti az eredményeket.
      </h1>
    </Transition>
    <Transition>
      <h1 class="failure" v-if="showErrorMessage && !loaded">
        Hiba történt. Nem sikerült a választ regisztrálni.
      </h1>
    </Transition>
  </div>
</template>
<script>
import { PushData } from "../utils/fetchdata";
import "../assets/transition.css";
import "../assets/loading.css";
import "bootstrap";

export default {
  props: ["formId"],
  data() {
    return {
      formData: null,
      loaded: false,
      isLoading: false,
      submitSuccess: false,
      showSuccesfullMessage: false,
      showErrorMessage: false,
      formId: 0,
      customAnswers: "Alap",
      answerCounter: 0,
      formValues: null,
    };
  },
  async created() {
    setTimeout(() => {
      this.loaded = true;
    }, 500);
  },
  methods: {
    GetAnswers() {
      let answers = [];
      for (let index = 1; index <= this.answerCounter; index++) {
        const element = this.formValues["answer_" + index];
        console.log(element, index, this.answerCounter);
        answers.push(element);
      }
      return [answers.join(", ")];
    },
    handleForm() {
      if (this.value.length == 0) {
        return;
      }
      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      let questionType = 0;
      switch (this.customAnswers) {
        case "Checkbox":
          questionType = 1;
          break;
        case "Szöveg":
          questionType = 2;
          break;
        case "Rádió":
          questionType = 3;
          break;
        default:
          questionType = 3;
          break;
      }
      let answers = this.GetAnswers();
      console.log(answers);
      var raw = JSON.stringify({
        questions: [this.value],
        questionTypes: [questionType],
        answers,
      });

      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
      };

      this.loaded = false;
      setTimeout(() => {
        this.isLoading = true;
      }, 500);

      PushData(requestOptions)
        .then((response) => response.json())
        .then((result) => {
          console.log(result);
          this.formId = result.id;
          setTimeout(() => {
            this.submitSuccess = true;
          }, 2000);
          setTimeout(() => {
            this.isLoading = false;
          }, 3000);
          setTimeout(() => {
            this.showSuccesfullMessage = true;
          }, 3500);
        })
        .catch((error) => {
          console.error(error);
          this.isLoading = false;
          setTimeout(() => {
            this.isLoading = false;
            this.submitSuccess = false;
            this.showErrorMessage = true;
          }, 500);
        });
    },
  },
};
</script>
<style>
@media (min-width: 1024px) {
  .kerdes {
    min-height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
  }
}
.form-margin-top {
  margin-top: 1rem;
}
.buttons {
  display: flex;
  justify-content: space-around;
  margin-bottom: 1rem;
}
</style>
<style scoped src="bootstrap/dist/css/bootstrap.min.css">
</style>
