<script setup>
import { ref, computed, watch } from "vue";
import {
  PredmetAPI,
  IspitAPI,
  IspitniRokAPI,
  ZapisnikAPI,
  StudentPredmetAPI
} from "../api/api.js";

const props = defineProps({
  studentId: Number
});

const zapisnici = ref([]);
const ispiti = ref([]);
const predmeti = ref([]);
const rokovi = ref([]);
const studentPredmeti = ref([]);

async function loadData() {
  if (!props.studentId) return;

  const [
    zRes,
    iRes,
    pRes,
    rRes,
    spRes
  ] = await Promise.all([
    ZapisnikAPI.getAll(),
    IspitAPI.getAll(),
    PredmetAPI.getAll(),
    IspitniRokAPI.getAll(),
    StudentPredmetAPI.getAll()
  ]);

  zapisnici.value = zRes.data.filter(
    z => Number(z.ID_STUDENTA) === Number(props.studentId)
  );

  ispiti.value = iRes.data;
  predmeti.value = pRes.data;
  rokovi.value = rRes.data;
  studentPredmeti.value = spRes.data;
}

watch(() => props.studentId, loadData, { immediate: true });


function getIspit(idIspita) {
  return ispiti.value.find(i => i.ID_ISPITA === idIspita);
}

function predmetNaziv(idPredmeta) {
  return predmeti.value.find(p => p.ID_PREDMETA === idPredmeta)?.NAZIV || "—";
}

function rokNaziv(idRoka) {
  return rokovi.value.find(r => r.ID_ROKA === idRoka)?.NAZIV || "—";
}

function skolskaGodina(idPredmeta) {
  return studentPredmeti.value.find(
    sp =>
      sp.ID_STUDENTA === props.studentId &&
      sp.ID_PREDMETA === idPredmeta
  )?.SKOLSKA_GODINA || "—";
}

const prosek = computed(() => {
  if (!zapisnici.value.length) return "—";

  const polozeniPoPredmetu = {};

  zapisnici.value.forEach(z => {
    const ispit = getIspit(z.ID_ISPITA);
    if (!ispit) return;

    const idPredmeta = ispit.ID_PREDMETA;
    const ocena = Number(z.OCENA);

    if (ocena >= 6 ) {
      polozeniPoPredmetu[idPredmeta] = ocena;
    }
  });

  const ocene = Object.values(polozeniPoPredmetu);

  if (!ocene.length) return "—";

  const suma = ocene.reduce((s, o) => s + o, 0);
  return (suma / ocene.length).toFixed(2);
});


function uspesnost(idPredmeta) {
  if (!idPredmeta) return "—";

  const pokusaji = zapisnici.value.filter(z => {
    const ispit = getIspit(z.ID_ISPITA);
    return ispit && ispit.ID_PREDMETA === idPredmeta;
  });

  if (!pokusaji.length) return "—";

  const neuspesni = pokusaji.filter(
    z => Number(z.OCENA) < 6
  ).length;

  // ako je polozio iz prvog puta
  if (neuspesni === 0) return "100%";

  return (100 / (neuspesni + 1)).toFixed(2) + "%";
}


</script>

<template>
  <div class="passed">
    <h3>Položeni ispiti</h3>

    <table v-if="zapisnici.length">
      <thead>
        <tr>
          <th>Predmet</th>
          <th>Ocena</th>
          <th>Ispitni rok</th>
          <th>Školska godina</th>
          <th>Uspešnost</th>
        </tr>
      </thead>

      <tbody>
        <tr
          v-for="z in zapisnici"
          :key="z.ID_STUDENTA + '-' + z.ID_ISPITA"
        >
          <td>
            {{ predmetNaziv(getIspit(z.ID_ISPITA)?.ID_PREDMETA) }}
          </td>
          <td>{{ z.OCENA }}</td>
          <td>
            {{ rokNaziv(getIspit(z.ID_ISPITA)?.ID_ROKA) }}
          </td>
          <td>
            {{ skolskaGodina(getIspit(z.ID_ISPITA)?.ID_PREDMETA) }}
          </td>
          <td>
            {{ uspesnost(getIspit(z.ID_ISPITA)?.ID_PREDMETA) }}
          </td>
        </tr>
      </tbody>
    </table>

    <p v-else>Nema položenih ispita.</p>

    <div class="summary">
      <strong>Prosečna ocena:</strong> {{ prosek }}
    </div>
  </div>
</template>

<style scoped>
.passed {
  margin-top: 20px;
}

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

.summary {
  margin-top: 10px;
  font-size: 1.1em;
}
</style>
