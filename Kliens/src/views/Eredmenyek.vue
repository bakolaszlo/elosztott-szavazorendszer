<template>
  <div class="eredmenyek">
    <div class="container text-center mt-5 mb-5">
      <h1 class="mt-5 fw-bolder text-success">Az eddigi eredmények:</h1>
      <div class="table-responsive my-5">
        <!-- <Table v-if="loaded" :fields="fields" :tableData="formData"></Table> -->
      </div>
    </div>
  </div>
</template>

<style>
@media (min-width: 1024px) {
  .eredmenyek {
    min-height: 100vh;
    display: flex;
    align-items: center;
  }
}
</style>
