"use client";

import DataTable from "../../../components/DataTable/DataTable";
import { ProgramingLanguage, ProgramingLanguageClient } from "../../web-api-client";

export default function DashboardProgramingLanguage() {
  const ProgramingLanguageService = new ProgramingLanguageClient();

  return (
    <DataTable
      url="/programing-language"
      fields={Object.keys(new ProgramingLanguage().toJSON())}
      deleteAction={(id) => ProgramingLanguageService.deleteEntity(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        ProgramingLanguageService.getEntities(filters, sorts, page, pageSize)
      }
    />
  );
}
