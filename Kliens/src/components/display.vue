<template>
  <Transition>
    <div class="wrapper" v-if="isLoaded">
      <Bar :chart-data="chartData"> </Bar>
    </div>
  </Transition>
  <Transition>
    <div class="result-table-wrapper" v-if="isLoaded">
      <div>A következő emberek szavaztak:</div>
      <table class="table table-bordered table-striped">
        <tbody>
          <tr v-for="item in voters" :key="item">
            <td>
              {{ item[0] }}
            </td>
            <td>
              {{ item[1] }}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </Transition>
</template>

<script>
import { Bar } from "vue-chartjs";
import "bootstrap";
import "../assets/transition.css";

import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale,
} from "chart.js";
import { GetData } from "../utils/fetchdata";
import { track } from "@vue/reactivity";
import * as signalR from "@microsoft/signalr";

ChartJS.register(
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale
);
export default {
  name: "BarChart",
  components: {
    Bar,
  },
  data: () => ({
    isLoaded: false,
    chartData: {
      labels: ["Igen", "Nem", "Passz"],
      datasets: [
        {
          label: "Szavazatok",
          backgroundColor: "#f87979",
          data: [40, 20, 12],
        },
      ],
    },
    chartOptions: {
      responsive: true,
      maintainAspectRatio: false,
    },
    rawData: null,
    voters: null,
  }),
  methods: {
    parseData() {
      //Igen
      this.chartData.datasets[0].data[0] = this.rawData.filter(
        (obj) => obj.answers[0] === "Igen"
      ).length;

      // Nem
      this.chartData.datasets[0].data[1] = this.rawData.filter(
        (obj) => obj.answers[0] === "Nem"
      ).length;

      //Passz
      this.chartData.datasets[0].data[2] = this.rawData.filter(
        (obj) => obj.answers[0] === "Passz"
      ).length;
      let trackObj = {};
      this.rawData.forEach((cur) => {
        if (trackObj[cur.user] === undefined) {
          trackObj[cur.user] = 1;
        } else {
          trackObj[cur.user]++;
        }
      });
      let sortable = [];
      for (var user in trackObj) {
        sortable.push([user, trackObj[user]]);
      }
      sortable.sort(function (a, b) {
        return -(a[1] - b[1]);
      });
      this.voters = sortable;
    },
  },
  async mounted() {
    this.rawData = await GetData(
      "SubmittedForms/formId/" + this.$route.params.formId
    );
    this.parseData();
    this.isLoaded = true;
    var connection = new signalR.HubConnectionBuilder()
      .withUrl("/votehub")
      .build();

    connection
      .start()
      .then(function () {
        console.log("Connection established..");
      })
      .catch(function (err) {
        return console.error(err.toString());
      });

    connection.on("UpdateVote", (rawData, formId) => {
      if (formId != this.$route.params.formId) {
        return;
      }
      this.rawData.push(rawData);
      this.parseData();
    });
  },
};
</script>

<style scoped>
.wrapper {
  margin-top: 150px;
}
</style>
<style scoped src="bootstrap/dist/css/bootstrap.min.css"></style>
