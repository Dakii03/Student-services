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
});

const izborNapravljen = computed(() => {
  return selectedPredmet.value && selectedRok.value;
});

const ispitNePostoji = computed(() => {
  return izborNapravljen.value && !selectedIspit.value;
});

const indeksStudenta = (idStudenta) => {
  const s = studenti.value.find(
    st => Number(st.idStudenta) === Number(idStudenta)
  );

  if (!s) return "—";

  return `${s.smer}-${s.broj}/${s.godinaUpisa.slice(-2)}`;
};

// IZABRANI ISPIT
const selectedIspit = computed(() => {
  if (!selectedPredmet.value || !selectedRok.value) return null;
  zapisnikIzabran();

  return ispiti.value.find(
    i =>
      Number(i.idPredmeta) === Number(selectedPredmet.value) &&
      Number(i.idRoka) === Number(selectedRok.value)
  );
});

// CEO ZAPISNIK
const zapisnik = computed(() => {
  if (!selectedIspit.value) return [];

  return zapisnici.value.filter(
    z => Number(z.idIspita) === Number(selectedIspit.value.idIspita)
  );
});

// STATISTIKA
const statistika = computed(() => {
  const stats = { 6: 0, 7: 0, 8: 0, 9: 0, 10: 0 };

  zapisnik.value.forEach(z => {
    const ocena = Number(z.ocena);
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

    <!-- FILTERS -->
    <div class="filters">
      <div class="field">
        <label>Predmet</label>
        <select v-model="selectedPredmet">
          <option :value="null">-- izaberite --</option>
          <option
            v-for="p in predmeti"
            :key="p.idPredmeta"
            :value="p.idPredmeta"
          >
            {{ p.naziv }}
          </option>
        </select>
      </div>

      <div class="field">
        <label>Ispitni rok</label>
        <select v-model="selectedRok">
          <option :value="null">-- izaberite --</option>
          <option
            v-for="r in rokovi"
            :key="r.idRoka"
            :value="r.idRoka"
          >
            {{ r.naziv }} – {{ r.skolskaGod }}
          </option>
        </select>
      </div>
    </div>

    <!-- TABLE -->
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
          :key="z.idStudenta + '-' + z.idIspita"
        >
          <td>{{ indeksStudenta(z.idStudenta) }}</td>
          <td class="grade">{{ z.ocena }}</td>
        </tr>
      </tbody>
    </table>

    <!-- EMPTY STATES -->
    <p v-if="!izborNapravljen" class="empty">
      Izaberite predmet i ispitni rok.
    </p>

    <p v-else-if="ispitNePostoji" class="empty">
      Za izabrani ispitni rok nije bilo polaganja ispita iz izabranog predmeta.
    </p>

    <!-- STATS -->
    <div v-if="zapisnik.length" class="stats">
      <h4>Statistika ocena</h4>
      <ul>
        <li v-for="(v, k) in statistika" :key="k">
          Ocena {{ k }}:
          <span :class="{ highlight: v > 0 }">{{ v }}</span>
        </li>
      </ul>
    </div>
  </div>
</template>

<style scoped>
.exam-stat {
  padding: 20px;
  width: 100%;
}

h3 {
  margin-bottom: 14px;
  border-bottom: 1px solid #e5e7eb;
  padding-bottom: 6px;
}

/* FILTERS */
.filters {
  display: flex;
  gap: 20px;
  margin-bottom: 15px;
}

.field {
  display: flex;
  flex-direction: column;
  gap: 4px;
}

label {
  font-size: 13px;
  color: #6b7280;
}

select {
  padding: 6px 8px;
  border: 1px solid #d1d5db;
  border-radius: 4px;
  background: white;
}

/* TABLE */
table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 10px;
}

th,
td {
  border: 1px solid #e5e7eb;
  padding: 8px;
  text-align: left;
}

th {
  background: #f1f5f9;
  font-weight: 600;
}

.grade {
  font-weight: 500;
}

/* EMPTY */
.empty {
  margin-top: 20px;
  color: #6b7280;
  font-style: italic;
}

/* STATS */
.stats {
  margin-top: 20px;
  padding-top: 10px;
  border-top: 1px solid #e5e7eb;
}

.stats h4 {
  margin-bottom: 8px;
}

.stats ul {
  list-style: none;
  padding: 0;
}

.stats li {
  font-size: 14px;
  margin-bottom: 4px;
}

.highlight {
  font-weight: 600;
  color: #ef4444;
}
</style>
