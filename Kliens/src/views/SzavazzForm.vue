<template>
  <div class="szavazz">
    <h1>{{ formData }}</h1>
    "Hello"

    <FormKit type="form" help="Válasz beküldése" @submit="handleForm">
      <FormKit
        type="text"
        label="Neved:"
        v-model="name"
        validation="required"
      />
      <FormKit
        v-model="value"
        type="radio"
        :label="formData.questions[0]"
        :options="['Igen', 'Nem', 'Passz']"
        help="Kérem válasszon ki egyet!"
        validation="required"
      />
    </FormKit>
  </div>
</template>
<script>
import { GetData, SubmitForm } from "../utils/fetchdata";

export default {
  props: ["formId"],
  data() {
    return {
      formData: null,
    };
  },
  async created() {
    this.formData = await GetData("forms/" + this.$route.params.formId);
    console.log(this.formData);
  },
  methods: {
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

      SubmitForm(requestOptions);
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
  }
}
</style>
