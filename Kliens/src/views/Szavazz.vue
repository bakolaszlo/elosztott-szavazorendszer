<template>
  <div class="szavazz">
    <Transition>
      <div class="container text-center mt-5 mb-5" v-if="loaded">
        <h1 class="mt-5 fw-bolder text-success">Az eddigi kérdések:</h1>
        <div class="table-responsive my-5">
          <Table :fields="fields" :tableData="formData"></Table>
        </div>
      </div>
    </Transition>
  </div>
</template>
<script>
import { GetData } from "../utils/fetchdata";
import "bootstrap";
import Table from "../components/Table.vue";
import "../assets/transition.css";

export default {
  data() {
    return {
      formData: null,
      loaded: false,
    };
  },
  async created() {
    this.formData = await GetData("forms");
    this.parseFormData();
    this.loaded = true;
    console.log(this.formData);
  },
  methods: {
    parseFormData() {
      let formCopy = this.formData;
      for (let index = 0; index < this.formData.length; index++) {
        const element = this.formData[index];
        try {
          element.questions = element.questions[0];
        } catch {}
      }
      console.log(this.formData);
    },
  },
  components: {
    Table,
  },
  setup() {
    //An array of values for the data
    const fields = ["Szám", "Kérdés"];
    return { fields };
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
