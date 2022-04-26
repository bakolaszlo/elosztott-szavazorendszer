<template>
  <div class="kerdes">
    <Transition>
      <div v-if="loaded">
        <FormKit
          v-if="loaded"
          type="form"
          help="Kérdés beküldése"
          @submit="handleForm"
        >
          <FormKit
            v-model="value"
            type="text"
            label="Adja meg a kérdését"
            help="Irja be a kérdést!"
            validation="required|length:1,60"
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
    };
  },
  async created() {
    this.loaded = true;
  },
  methods: {
    handleForm() {
      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      var raw = JSON.stringify({
        questions: [this.value],
        questionTypes: [],
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
</style>
