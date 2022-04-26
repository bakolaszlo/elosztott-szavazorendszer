<template>
  <div class="szavazz">
    <Transition>
      <div v-if="loaded">
        <FormKit
          v-if="loaded"
          type="form"
          help="Válasz beküldése"
          @submit="handleForm"
        >
          <FormKit
            type="text"
            label="Neved:"
            v-model="name"
            validation="required|length:1,30"
          />
          <FormKit
            v-model="value"
            :type="questionType"
            :label="formData.questions[0]"
            :options="possibleAnswers"
            help="Kérem válasszon ki egyet!"
            validation="required"
          />
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
        Sikeresen regisztráltuk a választ. <br />
        <Router-link :to="'/eredmenyek/' + $route.params.formId"
          >Itt</Router-link
        >
        megtekintheti az eredményeket.
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
import { GetData, SubmitForm } from "../utils/fetchdata";
import "../assets/transition.css";
import "../assets/loading.css";

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
      possibleAnswers: ["Igen", "Nem", "Passz"],
    };
  },
  async created() {
    this.formData = await GetData("forms/" + this.$route.params.formId);
    console.log(this.formData);
    this.prepareData();
    this.loaded = true;
  },
  methods: {
    prepareData() {
      try {
        this.possibleAnswers = this.formData.answers[0].split(", ");
      } catch {
        this.possibleAnswers = ["Igen", "Nem", "Passz"];
      }
      if (this.possibleAnswers.length == 1) {
        this.possibleAnswers = ["Igen", "Nem", "Passz"];
      }
      switch (this.formData.questionTypes[0]) {
        case 1:
          this.questionType = "checkbox";
          break;
        case 2:
          this.questionType = "text";
          break;
        case 3:
          this.questionType = "radio";
          break;
      }
    },
    handleForm() {
      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      var raw = JSON.stringify({
        user: this.name,
        formId: this.$route.params.formId,
        answers: [this.value],
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

      SubmitForm(requestOptions)
        .then((response) => response.text())
        .then((result) => {
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
  .szavazz {
    min-height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
  }
}
.lds-roller {
  display: inline-block;
  position: relative;
  width: 80px;
  height: 80px;
}
.lds-roller div {
  animation: lds-roller 1.2s cubic-bezier(0.5, 0, 0.5, 1) infinite;
  transform-origin: 40px 40px;
}
.lds-roller div:after {
  content: " ";
  display: block;
  position: absolute;
  width: 7px;
  height: 7px;
  border-radius: 50%;
  background: green;
  margin: -4px 0 0 -4px;
}
.lds-roller div:nth-child(1) {
  animation-delay: -0.036s;
}
.lds-roller div:nth-child(1):after {
  top: 63px;
  left: 63px;
}
.lds-roller div:nth-child(2) {
  animation-delay: -0.072s;
}
.lds-roller div:nth-child(2):after {
  top: 68px;
  left: 56px;
}
.lds-roller div:nth-child(3) {
  animation-delay: -0.108s;
}
.lds-roller div:nth-child(3):after {
  top: 71px;
  left: 48px;
}
.lds-roller div:nth-child(4) {
  animation-delay: -0.144s;
}
.lds-roller div:nth-child(4):after {
  top: 72px;
  left: 40px;
}
.lds-roller div:nth-child(5) {
  animation-delay: -0.18s;
}
.lds-roller div:nth-child(5):after {
  top: 71px;
  left: 32px;
}
.lds-roller div:nth-child(6) {
  animation-delay: -0.216s;
}
.lds-roller div:nth-child(6):after {
  top: 68px;
  left: 24px;
}
.lds-roller div:nth-child(7) {
  animation-delay: -0.252s;
}
.lds-roller div:nth-child(7):after {
  top: 63px;
  left: 17px;
}
.lds-roller div:nth-child(8) {
  animation-delay: -0.288s;
}
.lds-roller div:nth-child(8):after {
  top: 56px;
  left: 12px;
}
@keyframes lds-roller {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>
