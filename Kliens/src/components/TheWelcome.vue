<script setup>
import WelcomeItem from "./WelcomeItem.vue";
import DocumentationIcon from "./icons/IconDocumentation.vue";
import ToolingIcon from "./icons/IconTooling.vue";
import EcosystemIcon from "./icons/IconEcosystem.vue";
import CommunityIcon from "./icons/IconCommunity.vue";
import SupportIcon from "./icons/IconSupport.vue";
</script>

<template>
  <WelcomeItem>
    <template #icon>
      <DocumentationIcon />
    </template>
    <template #heading>Szavazási oldal</template>

    Interaktív szavazórendszer, valós időben követhető eredménytáblázattal.
  </WelcomeItem>

  <WelcomeItem>
    <template #icon>
      <ToolingIcon />
    </template>
    <template #heading>Szerkesztés</template>

    Újabb kérdések hozzáadásához a <Router-link :to="'/kerdes'"
          >Kérdés</Router-link> menűpontba lépjen!
  </WelcomeItem>

  <WelcomeItem>
    <template #icon>
      <EcosystemIcon />
    </template>
    <template #heading>Eredmények</template>

    Ha szeretnénk bármelyik kérdéshez beküldött szavazatokat megtekinteni, az <Router-link :to="'/eredmenyek'"
          >Eredmények</Router-link> menűpontra kattintsunk!
  </WelcomeItem>

  <WelcomeItem>
    <template #icon>
      <CommunityIcon />
    </template>
    <template #heading>Szavazás leadása</template>

    Szavazatunk leadását a <Router-link :to="'/szavazz'"
          >Szavazz!</Router-link> menűn belűl kiválasztott kérdésekben tehetjük meg.
  </WelcomeItem>

  <WelcomeItem>
    <template #icon>
      <SupportIcon />
    </template>
    <template #heading>Készítők</template>
    Bakó László Mihály, Gábor Anita - Renáta, Juhos Imola, Stoica Attila, Török Csongor
  </WelcomeItem>
</template>
