"use client";

import DataTable from "../../../components/DataTable/DataTable";
import { CertificateClient, CertificateMapping } from "../../web-api-client";

export default function DashboardCertificateType() {
  const CertificateService = new CertificateClient();

  return (
    <DataTable
      url="/certificate-type"
      fields={Object.keys(new CertificateMapping().toJSON())}
      deleteAction={(id) => CertificateService.deleteEntity(id)}
      fetchAction={(filters, sorts, page, pageSize) =>
        CertificateService.getEntities(filters, sorts, page, pageSize)
      }
    />
  );
}
