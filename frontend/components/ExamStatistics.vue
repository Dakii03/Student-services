<script setup>
import { ref, computed, onMounted } from "vue";
import {
  IspitAPI,
  PredmetAPI,
  IspitniRokAPI,
  ZapisnikAPI
} from "../api/api";
import { StudentAPI } from "../api/api";
import { zapisnikIzabran } from "../store/notifications.js";

const studenti = ref([]);
const ispiti = ref([]);
const predmeti = ref([]);
const rokovi = ref([]);
const zapisnici = ref([]);

const selectedPredmet = ref(null);
const selectedRok = ref(null);

onMounted(async () => {
  const [iRes, pRes, rRes, zRes, sRes] = await Promise.all([
    IspitAPI.getAll(),
    PredmetAPI.getAll(),
    IspitniRokAPI.getAll(),
    ZapisnikAPI.getAll(),
    StudentAPI.getAll()
  ]);

  ispiti.value = iRes.data;
  predmeti.value = pRes.data;
  rokovi.value = rRes.data;
  zapisnici.value = zRes.data;
  studenti.value = sRes.data;

  console.log("STUDENTI:", studenti.value);
});

const izborNapravljen = computed(() => {
  return selectedPredmet.value && selectedRok.value;
});

const ispitNePostoji = computed(() => {
  return izborNapravljen.value && !selectedIspit.value;
});


const indeksStudenta = (idStudenta) => {
  const s = studenti.value.find(
    st => Number(st.ID_STUDENTA) === Number(idStudenta)
  );

  if (!s) return "—";

  return `${s.SMER}-${s.BROJ}/${s.GODINA_UPISA.slice(-2)}`;
};

// IZABRANI ISPIT (zapisnik)
const selectedIspit = computed(() => {
  if (!selectedPredmet.value || !selectedRok.value) return null;
  zapisnikIzabran();

  return ispiti.value.find(
    i =>
      Number(i.ID_PREDMETA) === Number(selectedPredmet.value) &&
      Number(i.ID_ROKA) === Number(selectedRok.value)
  );
});

// CEO ZAPISNIK ZA IZABRANI ISPIT
const zapisnik = computed(() => {
  if (!selectedIspit.value) return [];

  return zapisnici.value.filter(
    z => Number(z.ID_ISPITA) === Number(selectedIspit.value.ID_ISPITA)
  );
});

// STATISTIKA OCENA
const statistika = computed(() => {
  const stats = { 6: 0, 7: 0, 8: 0, 9: 0, 10: 0 };

  zapisnik.value.forEach(z => {
    const ocena = Number(z.OCENA);
    if (stats[ocena] !== undefined) {
      stats[ocena]++;
    }
  });

  return stats;
});
</script>

<template>
  <div class="exam-stat">
    <h3>Zapisnik ispita</h3>

    <div class="filters">
      <div>
        <label>Predmet: </label>
        <select v-model="selectedPredmet">
          <option :value="null">-- izaberite --</option>
          <option
            v-for="p in predmeti"
            :key="p.ID_PREDMETA"
            :value="p.ID_PREDMETA"
          >
            {{ p.NAZIV }}
          </option>
        </select>
      </div>
      <br/>
      <div>
        <label>Ispitni rok: </label>
        <select v-model="selectedRok">
          <option :value="null">-- izaberite --</option>
          <option
            v-for="r in rokovi"
            :key="r.ID_ROKA"
            :value="r.ID_ROKA"
          >
            {{ r.NAZIV }} – {{ r.SKOLSKA_GOD }}

          </option>
        </select>
      </div>
    </div>

    <table v-if="zapisnik.length">
      <thead>
        <tr>
          <th>Indeks</th>
          <th>Ocena</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="z in zapisnik"
          :key="z.ID_STUDENTA + '-' + z.ID_ISPITA"
        >
          <td>{{ indeksStudenta(z.ID_STUDENTA) }}</td>
          <td>{{ z.OCENA }}</td>
        </tr>
      </tbody>
    </table>


    <p v-if="!izborNapravljen" class="empty">
      Izaberite predmet i ispitni rok.
    </p>

    <p v-else-if="ispitNePostoji" class="empty">
      Za izabrani ispitni rok nije bilo polaganja ispita iz izabranog predmeta.
    </p>
     
    <div v-if="zapisnik.length" class="stats">
      <h4>Statistika ocena</h4>
      <ul>
        <li v-for="(v, k) in statistika" :key="k">
          Ocena {{ k }}: <span v-if="v" style="color: red;">{{ v }}</span>
          <span v-else>{{ v }}</span>
        </li>
      </ul>
    </div>
  </div>
</template>

<style scoped>
  table {
  width: 100%;
  border-collapse: collapse;
  }

  th,
  td {
    border: 1px solid #ddd;
    padding: 6px;
  }

  th {
    background: #f1f5f9;
  }
</style>